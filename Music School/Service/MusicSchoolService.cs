using Music_School.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Music_School.Configuration.MusicSchoolConfiguration;

namespace Music_School.Service
{
    internal static class MusicSchoolService
    {
        public static void CreateXMLIfNotExist()
        {
            if (!File.Exists(musicSchoolPath))
            {
                //create new document (xml)
                XDocument document = new();
                //create an element
                XElement musicSchool = new("music-school"); //Parse = <music-school> </music-school>
                //document add element
                document.Add(musicSchool);
                //document save changes to provided path
                document.Save(musicSchoolPath);
            }
        }

        public static void InsertClassroom(string classRoomName)
        {
            //load document
            XDocument document = XDocument.Load(musicSchoolPath);
            //find root
            XElement? musicSchool = document.Descendants("music-school").FirstOrDefault();
            //validate music-school existence
            if (musicSchool == null)
            {
                return;
            }
            //create new x element 
            //<class-room name="guitar one on one">
            XElement classRoom = new(
                "class-room",
                new XAttribute("name", classRoomName)
            );
            //add to music-school new class-room
            musicSchool.Add(classRoom);
            //document save
            document.Save(musicSchoolPath);
        }

        public static void AddTeacher(string classRoomName, string teacherName)
        {
            //load documents
            XDocument document = XDocument.Load(musicSchoolPath);
            //find specific class-room by classRoomName
            XElement? classRoom = document.Descendants("class-room")
                .FirstOrDefault(room => room.Attribute("name")?.Value == classRoomName);
            if (classRoom == null) 
            {
                return;
            }
            //create new x element with attribute name = teachername
            XElement teacher = new(
                "teacher",
                new XAttribute("name", teacherName)
            );
            //add teacher to the class-room
            classRoom.Add(teacher);
            //save document
            document.Save(musicSchoolPath);
        }

        public static void AddStudent(string classRoomName, string studentName, string instrumentName)
        {
            XDocument document = XDocument.Load(musicSchoolPath);
            XElement? classRoom = (from  room in document.Descendants("class-room")
                                   where room.Attribute("name")?.Value == classRoomName
                                   select room).FirstOrDefault();
            if (classRoom == null)
            {
                return;
            }
            XElement student = new(
                "student",
                new XAttribute("name", studentName),
                new XElement("instrument", instrumentName)
                );
            classRoom.Add(student);
            document.Save(musicSchoolPath);

        }

        private static XElement ConvertStudentToElement(Student student) =>
            new(
                "student",
                new XAttribute("name", student.Name),
                new XElement("instrument", student.instrument.Name)
                );
        public static void AddManyStudents(string classRoomName, params Student[] students)
        {
            XDocument document = XDocument.Load (musicSchoolPath);
            XElement? classRoom = document.Descendants("class-room")
                .FirstOrDefault(room => room.Attribute("name")?.Value == classRoomName);
            if (classRoom == null)
            {
                return;
            }
            List<XElement> studentsLists =
                students.Select(ConvertStudentToElement).ToList();

            classRoom.Add(studentsLists);
            document.Save(musicSchoolPath);

        }

        public static void UpdateInstrument
            (string prevstudent, string prevInstrument, string newStudent, string newInstrument)
        {
            XDocument document = XDocument.Load(musicSchoolPath);
            XElement? student = document.Descendants("student")
                .FirstOrDefault(room => room.Attribute("name")?.Value == prevstudent);
            if (student == null)
            {
                return;
            }
            student.SetAttributeValue("name", newStudent);
            student.SetElementValue(prevInstrument ,newInstrument);
            document.Save(musicSchoolPath);
        }

        public static void UpdateTeacher(string prevTeacher, string newTeacher)
        {
            XDocument document = XDocument.Load(musicSchoolPath);
            XElement? teacher = document.Descendants("teacher")
                .FirstOrDefault(teach => teach.Attribute("name")?.Value==prevTeacher);
            if (teacher == null)
            {
                return;
            }
            teacher.SetAttributeValue("name", newTeacher);
            document.Save(musicSchoolPath);
        }
    }
}
