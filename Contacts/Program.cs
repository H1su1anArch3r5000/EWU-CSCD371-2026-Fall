namespace Contacts;

public static class Program
{
    public static void Main()
    {
        var paperbackBook = new ContactBook();
        paperbackBook.Load();
        while (true) {
            Console.WriteLine("\nContacts CLI");
            Console.WriteLine("1. Add a Student");
            Console.WriteLine("2. Add an Employee");
            Console.WriteLine("3. List Contacts");
            Console.WriteLine("4. Remove a Contact"); //Note to self for future, try making this more user-interactive like an automated
            Console.WriteLine("5: Exit");
            Console.WriteLine("Choose an option: "); //game for other personal projects

            var storyPicker = Console.ReadLine();
            //Quick note: make methods for all options before continuing
            switch (storyPicker) {
                case "1":
                    AddStudent(paperbackBook);
                    break;
                case "2":
                    AddEmployee(paperbackBook);
                    break;
                case "3":

                    break;
                case "4":

                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
        
        paperbackBook.Save();
    }
    private static void AddStudent(ContactBook novel) {
        var name = ReadName();
        Console.Write("Major: ");
        var major = Console.ReadLine() ?? "";
        Console.Write("Graduation Year: ");
        var gradYear = Console.ReadLine() ?? "";
        var email = ReadEmails();
        var student = new Student(name, major, gradYear, email);
        if (!novel.Add(student))
            Console.WriteLine("Duplicate student - not added");
        else
            Console.WriteLine("Student added");
    }
    private static void AddEmployee(ContactBook report) {
        var name = ReadName();
        Console.Write("Department: ");
        var department = Console.ReadLine() ?? "";
        Console.Write("Company name: ");
        var companyName = Console.ReadLine() ?? "";
        var email = ReadEmails(); 
        var employee = new Employee(name, department, companyName, email);
        if (!report.Add(employee))
            Console.WriteLine("Duplicate employee - not added");
        else
            Console.WriteLine("Employee added");
    }
    //helper method for these functions regarding generating FullName
    private static FullName ReadName() {
        Console.Write("First Name: ");
        var firstName = Console.ReadLine() ?? "";
        Console.WriteLine("Last Name: ");
        var lastName = Console.ReadLine() ?? "";
        Console.Write("Middle name (completely optional): ");
        var middleName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(middleName))
            middleName = null;
        return new FullName(firstName, lastName, middleName);
    }
    //helper method regarding generating Emails
    private static IReadOnlyList<String> ReadEmails() { 
        var emails = new List<String>();
        Console.WriteLine("Enter email addresses (blank line to finish):");
        while (true) {
            Console.Write("Email: ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) {
                break;
            }
            emails.Add(input);
        }
        return emails;
    }
    private static void EditContact(ContactBook book) {
        Console.Write("Enter ID to edit: ");
        if(!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID. ID must be an INT to be valid. Ending Program Now.");
            return;
        }
        var contacter = book.GetAll().FirstOrDefault(c => c.ID == id);

        if (contacter is null)
        {
            Console.WriteLine("Contact not found, ending program.");
            return;
        }
        
        if(contacter is Student s)
        {
            EditStudent(book, s);
        }
        else if(contacter is Employee e)
        {
            EditEmployee(book, e);
        }
    }
    //EditStudent Helper Method
    private static void EditStudent(ContactBook c, Student s)
    {
        Console.Write("New Major (blank = keep): ");
        var major = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(major))
            major = s.Major;

        Console.Write("New Grad Year (blank = keep): ");
        var grad_y = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(grad_y))
            grad_y = s.grad_year;
        Console.WriteLine("Update emails? (y/n): ");
        var updateEmails = Console.ReadLine()?.Trim().ToLower() == "y";

        var emails = updateEmails ? ReadEmails() : s.Emails;

        var updated_Student = s with
        {
            Major = major,
            grad_year = grad_y,
            Emails = emails
        };
        

        c.Remove(s);
        c.Add(updated_Student);

        Console.WriteLine("Student updated.");
    }
    //EditEmployee Helper Method
    private static void EditEmployee(ContactBook c, Employee e) {
        Console.Write("New Department (blank = keep): ");
        var department = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(department))
            department = e.Department;

        Console.Write("New Company (blank = keep): ");
        var comp = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(comp))
            comp = e.Company_Name;

        Console.WriteLine("Update emails? (y/n): ");
        var updateEmails = Console.ReadLine()?.Trim().ToLower() == "y";

        var emails = updateEmails ? ReadEmails() : e.Emails;

        var updated_Employee = e with
        {
            Department = department,
            Company_Name = comp,
            Emails = emails
        };

        c.Remove(e);
        c.Add(updated_Employee);

        Console.WriteLine("Employee updated.");
    }
    
}
