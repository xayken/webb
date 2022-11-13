using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp4
{
    class Program
    {
        static List<Student> studentList = new List<Student>();
        static List<Season> seasonList = new List<Season>();
        static List<Teacher> teacherList = new List<Teacher>();
        static List<Course> courseList = new List<Course>();
        static List<TeacherCourse> teacherCourseList = new List<TeacherCourse>();
        static List<StudentCourse> studentCourseList = new List<StudentCourse>();

        static void Load()
        {
            studentList.Add(new Student() { Name = "Serkan", Surname = "Kalaycıoğlu", StudentId = 1 });
            studentList.Add(new Student() { Name = "Emre", Surname = "Orhan", StudentId = 2 });
            studentList.Add(new Student() { Name = "Gökçe", Surname = "Marangoz", StudentId = 3 });

            courseList.Add(new Course() { CourseId = 1, CourseName = "OS" });
            courseList.Add(new Course() { CourseId = 2, CourseName = "VP" });
            courseList.Add(new Course() { CourseId = 3, CourseName = "OOP" });

            teacherList.Add(new Teacher() { Name = "İhsan", Surname = "İçiuyan", TeacherId = 1 });
            teacherList.Add(new Teacher() { Name = "Ahmet", Surname = "Acar", TeacherId = 2 });
            teacherList.Add(new Teacher() { Name = "Selçuk", Surname = "Şener", TeacherId = 3 });

            seasonList.Add(new Season() { SeasonId = 1, SeasonName = "22-23 W" });

            teacherCourseList.Add(new TeacherCourse() { CourseId = 1, SeasonId = 1, TeacherId = 2, TeacherCourseId = 1 });
            teacherCourseList.Add(new TeacherCourse() { CourseId = 2, SeasonId = 1, TeacherId = 3, TeacherCourseId = 2 });

            studentCourseList.Add(new StudentCourse() { StudentId = 1, TeacherCourseId = 1, StudentCourseId = 1 });
            studentCourseList.Add(new StudentCourse() { StudentId = 2, TeacherCourseId = 2, StudentCourseId = 2 });
        }
        static void Main(string[] args)
        {
            Load();
            Console.WriteLine("Seasons are listed, choose one of them");
            foreach (var season in seasonList)
            {
                Console.WriteLine(season.SeasonName);
            }
            var selectedSeasonName = Console.ReadLine();
            var selectedSeason = GetSeasonByName(selectedSeasonName);
            if (selectedSeason != null)
            {
                var seasonCourseList = GetCoursesBySeasonId(selectedSeason.SeasonId);
                Console.WriteLine("Season courses are listed choose one of them");
                foreach (var seasonCourse in seasonCourseList)
                {
                    Console.WriteLine(seasonCourse.CourseName);
                }
                var selectedCourse = Console.ReadLine();
                var course = GetCourseByName(selectedCourse);
                if (course != null)
                {
                    var teacherCourse = GetTeacherCourseByCourseIdAndSeasonId(course.CourseId, selectedSeason.SeasonId);
                    if (teacherCourse != null)
                    {
                        var teacher = GetTeacherById(teacherCourse.CourseId);
                        Console.WriteLine(teacher.Name + " " + teacher.Surname);
                        Console.WriteLine("------------------------");
                        var studentCourseList = GetStudentListByTeacherCourseId(teacherCourse.CourseId);
                        foreach (var studentCourse in studentCourseList)
                        {
                            Console.WriteLine(studentCourse.Name + " " + studentCourse.Surname);
                        }

                    }
                }
            }
        }

        static List<Student> GetStudentListByTeacherCourseId(int _teacherCourseId)
        {
            var studentCourses = studentCourseList.Where(x => x.TeacherCourseId == _teacherCourseId);
            List<Student> studentReturnList = new List<Student>();
            foreach (var studentCourse in studentCourses)
            {
                var student = studentList.FirstOrDefault(x => x.StudentId == studentCourse.StudentId);
                if (student != null)
                {
                    studentReturnList.Add(student);
                }
            }
            return studentReturnList;
        }

        static TeacherCourse GetTeacherCourseByCourseIdAndSeasonId(int _courseId, int _seasonId)
        {
            var teacherCourse = teacherCourseList.FirstOrDefault(x => x.CourseId == _courseId && x.SeasonId == _seasonId);
            return teacherCourse;
        }

        static Season GetSeasonByName(string _name)
        {
            var season = seasonList.FirstOrDefault(x => x.SeasonName == _name);
            return season;

        }

        static Teacher GetTeacherById(int _id)
        {
            var teacher = teacherList.FirstOrDefault(x => x.TeacherId == _id);
            return teacher;
        }

        static StudentCourse GetStudentCourseById(int _id)
        {
            var studentCourse = studentCourseList.FirstOrDefault(x => x.TeacherCourseId == _id);
            return studentCourse;
        }

        static TeacherCourse GetTeacherCourseBySeasonId(int _id)
        {
            var teacherCourse = teacherCourseList.FirstOrDefault(x => x.SeasonId == _id);
            return teacherCourse;
        }

        static Course GetCourseByName(string _name)
        {
            var course = courseList.FirstOrDefault(x => x.CourseName == _name);
            return course;
        }

        static Course GetCourseById(int _id)
        {
            var course = courseList.FirstOrDefault(x => x.CourseId == _id);
            return course;
        }

        static List<Course> GetCoursesBySeasonId(int _id)
        {
            var teacherCourses = teacherCourseList.Where(x => x.SeasonId == _id);
            List<Course> courseReturnList = new List<Course>();
            foreach (var teacherCourse in teacherCourses)
            {
                var course = GetCourseById(teacherCourse.CourseId);
                if (course != null)
                {
                    courseReturnList.Add(course);
                }
            }
            return courseReturnList;
        }
    }
}
