//IFD-5/1_Undzėnas_Ernestas_U5-12
//U5-12. Dėstytojai
//Pirmo failo pirmoje failo eilutėje nurodytas dėstytojų kiekis,
//mėnesio dienų skaičius, vidutinis dėstytojo mėnesio valandų krūvis.
//Tolesnėse failo eilutėse nurodyta informacija apie dėstytojus:
//dėstytojo pavardė, vardas, fakultetas, katedra. Kitame faile
//pateikta informacija apie dėstytojų skaitytų paskaitų kiekį
//kiekvieną dieną: dėstytojai(eilutės), valandų kiekis per dieną
//(stulpeliai). Nustatykite, aryra dienų, kai paskaitas skaitė visi
//dėstytojai, jei taip, kurios tai dienos. Surikiuokite dėstytojus
//pagal fakultetus. Nustatykite, kiekvieno fakulteto dėstytojų dirbtų
//valandų skaičių. Nustatykite, ar yra dėstytojų, kurie dirbo mažiau,
//nei vidutinis mėnesio dėstytojo darbo krūvis. Jei taip, kurie tai dėstytojai


using System;
using System.Collections.Generic;
using System.IO; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U5_12_ERNESTAS_UNDZĖNAS
{

    /// <summary>
    /// Class for lecturer data
    /// </summary>
    class Lecturer
    {
        private string lastName,//lecturers last name;
            firstName,          //lecturers first name;
            faculty,            //lecturers faculty;
            department;         //lecturers department;
        private int hours;      //lecturers hours worked;
        
        /// <summary>
        /// Constructor without parameters
        /// </summary>
        public Lecturer()
        {
            lastName = "";
            firstName = "";
            faculty = "";
            department = "";
            hours = 0;
        }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="lastName">lastname</param>
        /// <param name="firstName">firstname</param>
        /// <param name="faculty">faculty</param>
        /// <param name="department">department</param>
        /// <param name="hours">hours worked</param>
        public Lecturer(string lastName, string firstName, string faculty, 
            string department, int hours)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.faculty = faculty;
            this.department = department;
            this.hours = hours;
        }
        /// <summary>
        /// Method to add a lecturer
        /// </summary>
        /// <param name="lastName">lastname</param>
        /// <param name="firstName">firstname</param>
        /// <param name="faculty">faculty</param>
        /// <param name="department">department</param>
        public void Add(string lastName, string firstName, string faculty, 
            string department)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.faculty = faculty;
            this.department = department;
        }
        /// <summary>
        /// Method to add hours
        /// </summary>
        /// <param name="hrs">hours</param>
        public void AddHours(int hrs) { hours = hrs; }

        /// <summary>
        /// Returns last name
        /// </summary>
        /// <returns>last name</returns>
        public string GetLastName() { return lastName; }
        /// <summary>
        /// Returns first name
        /// </summary>
        /// <returns>first name</returns>
        public string GetFirstName() { return firstName; }
        /// <summary>
        /// Returns faculty
        /// </summary>
        /// <returns>faculty</returns>
        public string GetFaculty() { return faculty; }
        /// <summary>
        /// Returns department
        /// </summary>
        /// <returns>department</returns>
        public string GetDepartment() { return department; }

        /// <summary>
        /// Returns hours
        /// </summary>
        /// <returns>hours</returns>
        public int GetHours() { return hours; }

        /// <summary>
        /// Printing function for each lectuter
        /// </summary>
        /// <returns>each lecturers data</returns>
        public override string ToString()
        {
            string eilute;
            eilute = string.Format("{0, -15} {1, -10} {2, -15} {3, -20} {4, -22:d}",
            lastName, firstName, faculty, department, hours);
            return eilute;
        }

        /// <summary>
        /// Override method for <= operation 
        /// </summary>
        /// <param name="first">first lecturer</param>
        /// <param name="second">second lecturer</param>
        /// <returns></returns>
        public static bool operator <=(Lecturer first, Lecturer second)
        {
            int position = String.Compare(first.faculty,
                second.faculty, StringComparison.CurrentCulture);
            if (position > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Override method for >= operation
        /// </summary>
        /// <param name="first">first lecturer</param>
        /// <param name="second">second lecturer</param>
        /// <returns></returns>
        public static bool operator >=(Lecturer first, Lecturer second)
        {
            int position = String.Compare(first.faculty,
               second.faculty, StringComparison.CurrentCulture);
            if (position < 0)
                return true;
            return false;
        }


    }

    /// <summary>
    /// Class for university (used to store all lecturers)
    /// </summary>
    class University
    {
        const int CMaxMk = 1000;                //max number of lecturers
        public int n { get; set; }              //number of lecturers
        private Lecturer[] Lecturers;           //lecturer data
        public int averageHours { get; set; }   //average number of monthly hours

        /// <summary>
        /// Constructor without params
        /// </summary>
        public University()
        {
            n = 0;
            averageHours = 0;
            Lecturers = new Lecturer[CMaxMk];
        }

        /// <summary>
        /// A deep copy for a selected index lecturer
        /// </summary>
        /// <param name="nr">index</param>
        /// <returns></returns>
        public Lecturer GetLecturer(int nr)
        {
            Lecturer object1 = new Lecturer(
                Lecturers[nr].GetLastName(), Lecturers[nr].GetFirstName(),
                Lecturers[nr].GetFaculty(), Lecturers[nr].GetDepartment(),
                Lecturers[nr].GetHours()
                );
            return object1;
        }
        /// <summary>
        /// A deep copy + adds a new lecturer and also 
        /// increases the list by 1
        /// </summary>
        /// <param name="lecturer"></param>
        public void Add(Lecturer lecturer)
        {
            Lecturer lecturer1 = new Lecturer(
                lecturer.GetLastName(), lecturer.GetFirstName(),
                lecturer.GetFaculty(), lecturer.GetDepartment(),
                lecturer.GetHours());
            Lecturers[n++] = lecturer1;
        }

        /// <summary>
        /// A deep copy + changes selected index's lecturer
        /// </summary>
        /// <param name="nr"></param>
        /// <param name="lecturer"></param>
        public void ChangeLecturer(int nr, Lecturer lecturer)
        {
            Lecturer lecturer1 = new Lecturer(
                lecturer.GetLastName(), lecturer.GetFirstName(),
                lecturer.GetFaculty(), lecturer.GetDepartment(),
                lecturer.GetHours());
            Lecturers[nr] = lecturer1;
        }
    }
    /// <summary>
    /// Class to save working hours for each lecturer/day
    /// </summary>
    class Matrix
    {
        const int CMaxLecturers = 1000;             //max number of lecturers
        const int CMaxDays = 30;                    //max number of working days
        public int numberOfLecturers { get; set; }  //number of lecturers
        public int numberOfDays { get; set; }       //number of days
        private int[,] workHours;                   //working hours 

        /// <summary>
        /// constructor without params
        /// </summary>
        public Matrix()
        {
            numberOfLecturers = 0;
            numberOfDays = 0;
            workHours = new int[CMaxLecturers, CMaxDays];
        }


        /// <summary>
        /// Changes work hours matrix element
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="hours"></param>
        public void AddWorkHours(int i, int j, int hours) 
        { workHours[i, j] = hours; }

        /// <summary>
        /// Returns value of work hour matrix
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public int GetWorkHours(int i, int j) 
        { return workHours[i, j]; }

        /// <summary>
        /// Swaps 2 rows in the matrix
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        public void SwapRows(int row1, int row2)
        {
            for(int j = 0; j < numberOfDays; j++)
            {
                int d = workHours[row1, j];
                workHours[row1, j] = workHours[row2, j];
                workHours[row2, j] = d;
            }
        }


    }
    internal class Program
    {

        const string CFd = "..\\..\\Duomenys.txt";
        const string CFd1 = "..\\..\\Duomenys1.txt";
        const string CFr = "..\\..\\Rezultatai.txt";
        static void Main(string[] args)
        {
            University university = new University();
            Matrix universityMatrix = new Matrix();
            University universityWithLessHours = new University();

            ReadLecturers(CFd, university, universityMatrix);
            ReadWorkHours(CFd1, universityMatrix);

           
            
            if(File.Exists(CFr))
                File.Delete(CFr);

            using (var writer = File.CreateText(CFr))
            {
                writer.WriteLine("Starting data");
                writer.WriteLine();
                writer.WriteLine("Number of lecturers {0}", university.n);
                writer.WriteLine("Number of days {0}", 
                    universityMatrix.numberOfDays);
                writer.WriteLine();
            }

            Print(CFr, university, "Lecturers");
            PrintHours(CFr, universityMatrix, "Working hours");


            UpdateLecturerHours(university, universityMatrix);
            Print(CFr, university, "Updated lecturer working hours");

            //Task 1
            PrintDaysWhenAllWorked(CFr, universityMatrix);

            //task 2
            SortByFaculty(university, universityMatrix);
            Print(CFr, university, "Sorted by faculty name");

            //Task 3
            string[] faculties = new string[university.n];
            int[] facultyHours = new int[university.n];

            int facultyCount = FacultyHours(university, 
                faculties, facultyHours);
            PrintEachFacultyHours(CFr, faculties, 
                facultyHours, facultyCount);


            //Task 4
            LecturersLessAvgHours(university, universityWithLessHours);
            Print(CFr, universityWithLessHours,
                 "\n\nLecturers with less than " + university.averageHours +
                 " average monthly hours"
               );

        }

        /// <summary>
        /// Method to write data in a container
        /// </summary>
        /// <param name="fd"></param>
        /// <param name="university"></param>
        /// <param name="workHours"></param>
        static void ReadLecturers(string fd, University university, 
            Matrix workHours)
        {
            string lastName,
                firstName,
                faculty,
                department,
                line;

            int averageHours,
                numberOfLecturers,
                daysLectured;
            using(StreamReader reader = new StreamReader(fd))
            {
                line = reader.ReadLine();
                string[] parts = line.Split(';');
                numberOfLecturers = int.Parse(parts[0]);
                daysLectured = int.Parse(parts[1]);
                averageHours = int.Parse(parts[2]);
                workHours.numberOfLecturers = numberOfLecturers;
                workHours.numberOfDays = daysLectured;
                university.averageHours = averageHours;

                for (int i = 0; i < numberOfLecturers; i++)
                {
                    line = reader.ReadLine();
                    string[] partsEach = line.Split(';');
                    lastName = partsEach[0];
                    firstName = partsEach[1];
                    faculty = partsEach[2];
                    department = partsEach[3];
                    Lecturer lecturer = new Lecturer();
                    lecturer.Add(lastName, firstName, faculty, 
                        department);
                    university.Add(lecturer);
                }
            }
        }

        /// <summary>
        /// Method to write data in matrix working hours
        /// </summary>
        /// <param name="fd"></param>
        /// <param name="workHours"></param>
        static void ReadWorkHours(string fd, Matrix workHours)
        {
            int hours;
            string line;
            using(StreamReader reader = new StreamReader(fd))
            {
                for(int i = 0; i < workHours.numberOfLecturers; i++)
                {
                    line = reader.ReadLine();
                    string[] parts = line.Split(' ');
                    for(int j = 0; j < workHours.numberOfDays; j++)
                    {
                        hours = int.Parse(parts[j]);
                        workHours.AddWorkHours(i, j, hours);
                    }
                }
            }
        }
        /// <summary>
        /// Method to print containers data
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="university"></param>
        /// <param name="headline"></param>
        static void Print(string fv, University university, string headline)
        {
            using (var fr = File.AppendText(fv))
            {
                string line = new string('-', 70);
                fr.WriteLine(headline);
                fr.WriteLine();
                fr.WriteLine(line);
                fr.WriteLine(" Nr. Lastname       Name " +
                    "       Faculty         Department " +
                    "      Hours");
                fr.WriteLine(line);
                for (int i = 0; i < university.n; i++)
                    fr.WriteLine(" {0}. {1} ", i + 1, 
                        university.GetLecturer(i).ToString());
                fr.WriteLine(line);
                fr.WriteLine();
            }
        }

        /// <summary>
        /// Method to print value in the working hours matrix
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="universityMatrix"></param>
        /// <param name="headline"></param>
        static void PrintHours(string fv, Matrix universityMatrix, 
            string headline)
        {
            using (var fr = File.AppendText(fv))
            {
                string line = new string('-', 56);
                fr.WriteLine("{0} in {1} days.", 
                    headline, universityMatrix.numberOfDays);
                fr.WriteLine(line);
                for (int i = 0; i < universityMatrix.numberOfLecturers; i++)
                {
                    fr.Write("| Lecturer {0,2:d}.|", i + 1);
                    for (int j = 0; j < universityMatrix.numberOfDays; j++)
                        if(j == universityMatrix.numberOfDays - 1)
                        {
                            fr.Write("{0,3:d} |", 
                                universityMatrix.GetWorkHours(i, j));
                        }
                        else
                        {
                            fr.Write("{0,3:d} ", 
                                universityMatrix.GetWorkHours(i, j));
                        }
                    
                    fr.WriteLine();
                }
                fr.WriteLine(line);
            }
        }

        /// <summary>
        /// Method to update each lecturer with hours from matrix
        /// </summary>
        /// <param name="university"></param>
        /// <param name="universityMatrix"></param>
        static void UpdateLecturerHours(University university, 
            Matrix universityMatrix)
        {
            int sum;
            Lecturer lecturer;
            for (int i = 0; i < university.n; i++)
            {
                sum = 0;
                for (int j = 0; j < universityMatrix.numberOfDays; j++)
                    sum = sum + universityMatrix.GetWorkHours(i, j);
                lecturer = university.GetLecturer(i);
                lecturer.AddHours(sum);
                university.ChangeLecturer(i, lecturer);
            }
        }
        /// <summary>
        /// Method to add each lecturer with required amount 
        /// of hours
        /// </summary>
        /// <param name="university"></param>
        /// <param name="universityNoDaysOff"></param>
        static void LecturersLessAvgHours(University university,
            University universityNoDaysOff)
        {
            for(int i = 0; i < university.n; i++)
            {
                Lecturer lecturer = university.GetLecturer(i);
                if(lecturer.GetHours() <= university.averageHours)
                {
                    universityNoDaysOff.Add(lecturer);
                }
            }
        }

        /// <summary>
        /// method used to sort each faculty by name ascending
        /// </summary>
        /// <param name="university"></param>
        /// <param name="universityMatrix"></param>
        static void SortByFaculty(University university, 
            Matrix universityMatrix)
        {
            Lecturer lecturer;
            for (int i = 0; i < university.n - 1; i++)
            {
                int minnr = i;
                for (int j = i + 1; j < university.n; j++)
                    if (university.GetLecturer(j) >= 
                        university.GetLecturer(minnr))
                        minnr = j;
                lecturer = university.GetLecturer(i);
                university.ChangeLecturer(i, university.
                    GetLecturer(minnr));
                university.ChangeLecturer(minnr, lecturer);
                universityMatrix.SwapRows(i, minnr);
            }
        }


        /// <summary>
        /// Method that returns all days that everyone worked
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        static bool AllLecturersWorkedThatDay(Matrix matrix, int day)
        {
            for (int i = 0; i < matrix.numberOfLecturers; i++)
            {
                if (matrix.GetWorkHours(i, day) == 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Prints all days that all lecturers have worked 
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="universityMatrix"></param>
        static void PrintDaysWhenAllWorked(string fv, 
            Matrix universityMatrix)
        {
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine("Days when all lecturers worked:");
                fr.WriteLine("--------------------------------");
                bool worked = false;

                for(int j = 0; j < universityMatrix.numberOfDays; j++)
                {
                    if(AllLecturersWorkedThatDay(universityMatrix, j))
                    {
                        fr.WriteLine("Day {0} ", j + 1);
                        worked = true;
                    }
                }

                if (!worked)
                {
                    fr.WriteLine("No such day");
                }
                fr.WriteLine();
                fr.WriteLine();
            }
        }

        /// <summary>
        /// Method to return all faculties working hours
        /// </summary>
        /// <param name="university"></param>
        /// <param name="faculties"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        static int FacultyHours(University university, string[] faculties,
            int[] hours)
        {
            int k = 0;

            faculties[0] = university.GetLecturer(0).GetFaculty();
            hours[0] = university.GetLecturer(0).GetHours();
            k = 1;

            for (int i = 1; i < university.n; i++)
            {
                Lecturer lecturer = university.GetLecturer(i);
                if(lecturer.GetFaculty() == faculties[k - 1])
                {
                    hours[k - 1] += lecturer.GetHours();
                }
                else
                {
                    faculties[k] = lecturer.GetFaculty();
                    hours[k] = lecturer.GetHours();
                    k++;
                }

            }
            return k;
        }

        /// <summary>
        /// Method to print each faculty and its working hours
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="faculties"></param>
        /// <param name="hours"></param>
        /// <param name="count"></param>
        static void PrintEachFacultyHours(string fv, string[] faculties,
            int[] hours, int count)
        {
            using (var fr = File.AppendText(CFr))
            {
                fr.WriteLine("Total working hours by faculty:");
                fr.WriteLine("--------------------------------");


                for (int i = 0; i < count; i++)
                {
                    fr.WriteLine("{0,-20} {1,5} hours", 
                        faculties[i], hours[i]);
                }

                fr.WriteLine();
            }
        }


    }
}
