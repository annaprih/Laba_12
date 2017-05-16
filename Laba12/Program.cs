using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba12
{
    class Program
    {
        static void Main(string[] args)
        {
            Subjects temp = null;
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                
                using (var trans= dataBaseContext.Database.BeginTransaction())
                {
                    try
                    {
                        dataBaseContext.setSubjectses.Add(new Subjects
                        {
                            Name = "OOP",
                            NameOfLector = "Patsei",
                            CountOfLabs = 19,
                            CountOfLection = 25,
                            TypeOfControlling = "Exame"
                        });
                        dataBaseContext.SaveChanges();
                        temp = dataBaseContext.setSubjectses.Select(p => p).First();
                        dataBaseContext.setStudents.Add(new Students
                        {
                            Name = "Prykhach",
                            Subject = temp
                        });
                        dataBaseContext.setStudents.Add(new Students
                        {
                            Name = "Lomako",
                            Subject = new Subjects
                            {
                                Name = "DataBase",
                                NameOfLector = "Pustovalova",
                                CountOfLabs = 19,
                                CountOfLection = 25,
                                TypeOfControlling = "Exame"
                            }
                        });
                        dataBaseContext.setStudents.Add(new Students
                        {
                            Name = "Orlova",
                            Subject = temp
                        });
                        dataBaseContext.SaveChangesAsync().GetAwaiter().GetResult();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                       trans.Rollback();
                    }
                }
               


                foreach (var i in dataBaseContext.setStudents.Include("Subject"))
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine();


                dataBaseContext.setStudents.Where(a => a.Id == 2).First().Name = "Sazonova";
                dataBaseContext.SaveChanges();
                foreach (var i in dataBaseContext.setStudents)
                {
                    Console.WriteLine(i);
                    
                }
                Console.WriteLine();

                dataBaseContext.setStudents.Remove(dataBaseContext.setStudents.Where(a => a.Name == "Lomako").First());
                dataBaseContext.SaveChanges();
                foreach (var i in dataBaseContext.setStudents.Include("Subject"))
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine();

                
                foreach (var i in dataBaseContext.setStudents.OrderBy(a => a.Name).Include("Subject"))
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine();

                var iEnumerableStud =
                    (from q in dataBaseContext.setStudents where q.Name == "Prykhach" select q).ToList();
                foreach (var i in iEnumerableStud)
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine();


                var query = dataBaseContext.Database.SqlQuery<Subjects>("select * from Subjects");
                foreach (var i in query)
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine();
            }
            Repository<Students> repositoryStudents = new Repository<Students>(new DataBaseContext());
            foreach (var i in repositoryStudents.Get())
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();

            foreach (var i in repositoryStudents.Get(a=> a.Name == "Prykhach"))
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();

            Console.WriteLine(repositoryStudents.FindById(1));
            Console.WriteLine();
            Students stud = new Students
            {
                Name = "Shimanskaya",
                Subject = temp
            };
            repositoryStudents.Create(stud);

           
            foreach (var i in repositoryStudents.Get())
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();

            stud.Name = "Lalala";
            repositoryStudents.Update(stud);
            foreach (var i in repositoryStudents.Get())
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();

            repositoryStudents.Remove(stud);
            foreach (var i in repositoryStudents.Get())
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
