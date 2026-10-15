namespace Contacts;

public static class Program
{
    public static void Main()
    {
        var paperbackBook = new ContactBook();

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
                //case "4":

                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

    }
    private static void AddStudent(ContactBook novel) {
        var name = ReadName();
        Console.Write("Major: ");
        var major = Console.ReadLine() ?? "";
        Console.Write("Graduation Year: ");
        var gradYear = Console.ReadLine() ?? "";
        var student = new Student(name, major, gradYear);
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
        var employee = new Employee(name, department, companyName);
        if (!report.Add(employee))
            Console.WriteLine("Duplicate employee - not added");
        else
            Console.WriteLine("Employee added");
    }

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
}
