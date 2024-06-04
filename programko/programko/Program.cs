using System.Text.RegularExpressions;

//uloha 4

class Program
{
    static void Main()
    {
        string input = @"
            Some text before
            {START:block1}
                Some text inside block1
                {START:block2}
                    Some text inside block2
                {END:block2}
                Some text inside block1
            {END:block1}
            Some text after
        ";

        try
        {
            ValidateDocumentStructure(input);
            Console.WriteLine("Document structure is valid.");
        }
        catch
        {
            Console.WriteLine("Document structure is invalid");
        }
    }

    static void ValidateDocumentStructure(string input)
    {
        var stack = new Stack<string>();
        var regex = new Regex(@"\{(START|END):(\w+)\}");
        var matches = regex.Matches(input);

        foreach (Match match in matches)
        {
            string type = match.Groups[1].Value;
            string name = match.Groups[2].Value;

            if (type == "START")
            {
                stack.Push(name);
            }
            else if (type == "END")
            {
                if (stack.Count == 0 || stack.Peek() != name)
                {
                    throw new InvalidOperationException("Document structure is invalid");
                }
                stack.Pop();
            }
        }

        if (stack.Count > 0)
        {
            throw new InvalidOperationException("Document structure is invalid");
        }
    }
}
