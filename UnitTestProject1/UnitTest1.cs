﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        String filepath = @"C:\UnitTest1";

        [TestMethod]
        public void Destroy()
        {
            IEnumerable<String> files = Directory.EnumerateFiles(filepath);
            foreach(String file in files)
            {
                File.Delete(file);
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            TestDbContext ctx = new TestDbContext();
            Assert.IsTrue(String.Equals(ctx.DatabaseFolder,filepath));
        }

        [TestMethod]
        public void TestMethod2()
        {
            TestDbContext ctx = new TestDbContext();
            Student st1 = new Student();
            st1.age = 22;
            st1.name = "Deniz C.";
            ctx.students.Add(st1);
            ctx.SaveChanges();
            Assert.IsTrue(st1.id == 1);
        }

        [TestMethod]
        public void TestMethod3()
        {
            TestDbContext ctx = new TestDbContext();
            ctx.ReadAll();
            List<Student> studentsList = ctx.students.allRecords;
            Assert.IsTrue(studentsList.Count == 1);
        }

        [TestMethod]
        public void TestMethod4()
        {
            TestDbContext ctx = new TestDbContext();
            ctx.ReadAll();
            List<Student> studentsList = ctx.students.allRecords;
            Student deniz = studentsList
                .FirstOrDefault(student => student.name.StartsWith("Deniz"));

            Assert.IsTrue(deniz.age == 22);
        }

        [TestMethod]
        public void TestMethod5()
        {
            TestDbContext ctx = new TestDbContext();
            Student kerem = new Student();
            kerem.name = "Kerem O.";
            kerem.age = 21;
            ctx.students.Add(kerem);
            ctx.SaveChanges();
            Assert.IsTrue(kerem.id == 2);
        }

        [TestMethod]
        public void TestMethod6()
        {
            TestDbContext ctx = new TestDbContext();
            ctx.ReadAll();
            Assert.IsTrue(ctx.students.allRecords.Count == 2);
        }

        [TestMethod]
        public void TestMethod7()
        {
            TestDbContext ctx = new TestDbContext();
            Teacher teacher = new Teacher();
            teacher.name = "Bora U.";
            ctx.teachers.Add(teacher);
            ctx.SaveChanges();
            Assert.IsTrue(teacher.id == 1);
        }

        [TestMethod]
        public void TestMethod8()
        {
            TestDbContext ctx = new TestDbContext();
            ctx.ReadAll();
            Teacher teacher = ctx.teachers.allRecords
                .FirstOrDefault(t => t.name.StartsWith("Bora"));

            Assert.IsTrue(teacher.name.Equals("Bora U."));
        }

        [TestMethod]
        public void TestMethod9()
        {
            TestDbContext ctx = new TestDbContext();
            ctx.ReadAll();
            Teacher teacher = ctx.teachers.allRecords
                .FirstOrDefault(t => t.name.StartsWith("Bora"));
            Student deniz = ctx.students.allRecords.FirstOrDefault(s=>s.name.StartsWith("Deniz"));
            deniz.teacher = teacher;
            ctx.students.Update(deniz);
            ctx.SaveChanges();
        }

        [TestMethod]
        public void TestMethod10()
        {
            TestDbContext ctx = new TestDbContext();
            ctx.ReadAll();
            Teacher teacher = ctx.teachers.allRecords
                .FirstOrDefault(t => t.name.StartsWith("Bora"));
            
            Student kerem = ctx.students.allRecords.FirstOrDefault(s => s.name.StartsWith("Kerem"));
            kerem.teacher = teacher;
            ctx.students.Update(kerem);
            ctx.SaveChanges();
        }

        [TestMethod]
        public void TestMethod11()
        {
            TestDbContext ctx = new TestDbContext();
            ctx.ReadAll();
            Student deniz = ctx.students.allRecords.FirstOrDefault(s => s.name.StartsWith("Deniz"));
            Assert.IsTrue(deniz.teacher.name.Equals("Bora U."));
        }
        [TestMethod]
        public void TestMethod12()
        {
            TestDbContext ctx = new TestDbContext();
            ctx.ReadAll();
            Student deniz = ctx.students.allRecords.FirstOrDefault(s => s.name.StartsWith("Deniz"));
            Assert.IsTrue(deniz.teacher.name.Equals("Bora U."));
        }
        [TestMethod]
        public void TestMethod13()
        {
            TestDbContext ctx = new TestDbContext();
            Teacher t = new Teacher();
            t.name = "ismail k.";
            ctx.ReadAll();
            Student deniz = ctx.students.allRecords.FirstOrDefault(s => s.name.StartsWith("Deniz"));
            deniz.teacher = t;
            ctx.students.Update(deniz);
            ctx.SaveChanges();
            Assert.IsTrue(deniz.teacher.id == 2);
        }

        [TestMethod]
        public void TestMethod14()
        {
            TestDbContext ctx = new TestDbContext();
            ctx.ReadAll();
            Student deniz = ctx.students.allRecords.FirstOrDefault(s => s.name.StartsWith("Deniz"));
            ctx.teachers.Remove(deniz.teacher);
            ctx.SaveChanges();
            
            Teacher teacher = ctx.teachers.allRecords.FirstOrDefault(t => t.name.StartsWith("Bora"));
            Teacher teacher2 = ctx.teachers.allRecords.FirstOrDefault(t => t.name.StartsWith("ismail"));
            Assert.IsTrue(teacher.removed == false && teacher2.removed == true);
        }

        [TestMethod]
        public void TestMethod15()
        {
            TestDbContext ctx = new TestDbContext();
            ctx.ReadAll();
            Student deniz = ctx.students.allRecords.FirstOrDefault(s => s.name.StartsWith("Deniz"));
            Student kerem = ctx.students.allRecords.FirstOrDefault(s => s.name.StartsWith("Kerem"));
            Assert.IsTrue(deniz.teacher == null && kerem.teacher != null);
        }

        [TestMethod]
        public void TestMethod16()
        {
            TestDbContext ctx = new TestDbContext();
            ctx.ReadAll();
            Student deniz = ctx.students.allRecords.FirstOrDefault(s => s.name.StartsWith("Deniz"));
            Student kerem = ctx.students.allRecords.FirstOrDefault(s => s.name.StartsWith("Kerem"));
            Teacher teacher = ctx.teachers.allRecords.FirstOrDefault(t => t.name.StartsWith("Bora"));
            Assert.IsTrue(!teacher.student.Contains(deniz) && teacher.student.Contains(kerem));
        }

        [TestMethod]
        public void TestMethod17()
        {
            TestDbContext ctx = new TestDbContext();
            ctx.ReadAll();
            Student deniz = ctx.students.allRecords.FirstOrDefault(s => s.name.StartsWith("Deniz"));
            Student kerem = ctx.students.allRecords.FirstOrDefault(s => s.name.StartsWith("Kerem"));
            Teacher teacher = ctx.teachers.allRecords.FirstOrDefault(t => t.name.StartsWith("Bora"));
            Assert.IsTrue(!teacher.student.Contains(deniz) && teacher.student.Contains(kerem));
        }

        [TestMethod]
        public void TestMethod18()
        {
            TestDbContext ctx = new TestDbContext();
            ctx.ReadAll();
            Student deniz = ctx.students.allRecords.FirstOrDefault(s => s.name.StartsWith("Deniz"));
            Teacher teacher = new Teacher();
            teacher.name = "ali murat";
            teacher.student.Add(deniz);
            ctx.teachers.Add(teacher);
            ctx.SaveChanges();
            Assert.IsTrue(ctx.teachers.allRecords.Count == 2);
        }

        [TestMethod]
        public void TestMethod19()
        {
            TestDbContext ctx = new TestDbContext();
            ctx.ReadAll();
            Student kerem = ctx.students.allRecords.FirstOrDefault(s => s.name.StartsWith("Kerem"));
            Teacher teacher = ctx.teachers.allRecords.FirstOrDefault(t => t.name.StartsWith("ali"));
            teacher.student.Add(kerem);
            ctx.teachers.Update(teacher);
            ctx.SaveChanges();
            Assert.IsTrue(kerem.teacher == teacher);
        }

        [TestMethod]
        public void TestMethod20()
        {
            TestDbContext ctx = new TestDbContext();
            ctx.ReadAll();
            Student kerem = ctx.students.allRecords.FirstOrDefault(s => s.name.StartsWith("Kerem"));
            Teacher teacher = ctx.teachers.allRecords.FirstOrDefault(t => t.name.StartsWith("ali"));
            
            Assert.IsTrue(kerem.teacher == teacher);
        }

    }
}
