using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<List<object>> students = new List<List<object>>();
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add a student");
            Console.WriteLine("2. Remove a student by ID");
            Console.WriteLine("3. Update student information");
            Console.WriteLine("4. View all students");
            Console.WriteLine("5. Find student with the highest average grade");
            Console.WriteLine("6. Search students by name");
            Console.WriteLine("7. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddStudent(students);
                    break;
                case "2":
                    RemoveStudent(students);
                    break;
                case "3":
                    UpdateStudent(students);
                    break;
                case "4":
                    ViewStudents(students);
                    break;
                case "5":
                    FindTopStudent(students);
                    break;
                case "6":
                    SearchStudents(students);
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void AddStudent(List<List<object>> students)
    {
        Console.Write("Enter student ID: ");
        var id = Console.ReadLine();
        if (students.Any(s => s[0].ToString() == id))
        {
            Console.WriteLine("Duplicate ID. Student not added.");
            return;
        }

        Console.Write("Enter student name: ");
        var name = Console.ReadLine();
        Console.Write("Enter grades (comma-separated): ");
        var gradesInput = Console.ReadLine();
        var grades = gradesInput.Split(',').Select(g => Convert.ToDouble(g.Trim())).ToList();

        students.Add(new List<object> { id, name, grades });
        Console.WriteLine("Student added successfully.");
    }

    static void RemoveStudent(List<List<object>> students)
    {
        Console.Write("Enter student ID to remove: ");
        var id = Console.ReadLine();
        var student = students.FirstOrDefault(s => s[0].ToString() == id);

        if (student != null)
        {
            students.Remove(student);
            Console.WriteLine("Student removed successfully.");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    static void UpdateStudent(List<List<object>> students)
    {
        Console.Write("Enter student ID to update: ");
        var id = Console.ReadLine();
        var student = students.FirstOrDefault(s => s[0].ToString() == id);

        if (student != null)
        {
            Console.Write("Enter new student name (leave blank to keep current): ");
            var name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name))
            {
                student[1] = name;
            }

            Console.Write("Enter new grades (comma-separated, leave blank to keep current): ");
            var gradesInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(gradesInput))
            {
                var grades = gradesInput.Split(',').Select(g => Convert.ToDouble(g.Trim())).ToList();
                student[2] = grades;
            }

            Console.WriteLine("Student information updated successfully.");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    static void ViewStudents(List<List<object>> students)
    {
        if (students.Count == 0)
        {
            Console.WriteLine("No students found.");
            return;
        }

        Console.WriteLine("\nList of Students:");
        foreach (var student in students)
        {
            var id = student[0];
            var name = student[1];
            var grades = student[2] as List<object>;
            Console.WriteLine($"ID: {id}, Name: {name}, Grades: {string.Join(", ", grades.Select(g => g.ToString()))}");
        }
    }

    static void FindTopStudent(List<List<object>> students)
    {
        var topStudent = students
            .Select(s => new
            {
                ID = s[0],
                Name = s[1],
                AverageGrade = ((List<object>)s[2]).Average(g => Convert.ToDouble(g))
            })
            .OrderByDescending(s => s.AverageGrade)
            .FirstOrDefault();

        if (topStudent != null)
        {
            Console.WriteLine($"Top Student: ID: {topStudent.ID}, Name: {topStudent.Name}, Average Grade: {topStudent.AverageGrade:F2}");
        }
        else
        {
            Console.WriteLine("No students found.");
        }
    }

    static void SearchStudents(List<List<object>> students)
    {
        Console.Write("Enter name to search: ");
        var nameToSearch = Console.ReadLine().ToLower();

        var foundStudents = students.Where(s => s[1].ToString().ToLower().Contains(nameToSearch)).ToList();

        if (foundStudents.Count > 0)
        {
            Console.WriteLine("\nSearch Results:");
            foreach (var student in foundStudents)
            {
                var id = student[0];
                var name = student[1];
                var grades = student[2] as List<object>;
                Console.WriteLine($"ID: {id}, Name: {name}, Grades: {string.Join(", ", grades.Select(g => g.ToString()))}");
            }
        }
        else
        {
            Console.WriteLine("No students found with that name.");
        }
    }
}

