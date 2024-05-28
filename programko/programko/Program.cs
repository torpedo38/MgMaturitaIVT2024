
//uloha 3

UInt64 x = 123;
int ans = 0;
while(x > 0)
{
	if (x % 2 == 1)
	{
		ans++;
	}
	x >>= 1;
}
Console.WriteLine(ans);


//uloha 1

namespace EmailValidator
{
    public enum State
    {
        Start,
        LocalPart,
        AtSymbol,
        Domain,
        DomainDot,
        Error
    }

    public class EmailStateMachine
    {
        private State _currentState;
        private bool _hasDomain;

        public EmailStateMachine()
        {
            _currentState = State.Start;
        }

        public bool Validate(string email)
        {
            _currentState = State.Start;
            _hasDomain = false;

            foreach (var ch in email)
            {
                _currentState = NextState(_currentState, ch);
                if (_currentState == State.Error)
                {
                    return false;
                }
            }

            return _currentState == State.Domain && _hasDomain;
        }

        private State NextState(State currentState, char input)
        {
            switch (currentState)
            {
                case State.Start:
                    if (IsLetterOrDigit(input))
                        return State.LocalPart;
                    break;

                case State.LocalPart:
                    if (IsLetterOrDigit(input) || input == '.' || input == '_')
                        return State.LocalPart;
                    if (input == '@')
                        return State.AtSymbol;
                    break;

                case State.AtSymbol:
                    if (IsLetterOrDigit(input))
                    {
                        _hasDomain = true;
                        return State.Domain;
                    }
                    break;

                case State.Domain:
                    if (IsLetterOrDigit(input))
                        return State.Domain;
                    if (input == '.')
                        return State.DomainDot;
                    break;

                case State.DomainDot:
                    if (IsLetterOrDigit(input))
                        return State.Domain;
                    break;
            }

            return State.Error;
        }

        private bool IsLetterOrDigit(char ch)
        {
            return char.IsLetterOrDigit(ch);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var emailValidator = new EmailStateMachine();
            string[] testEmails =
            {
                "user@example.com",
                "user.name@example.com",
                "user@com",
                "user@.com",
                "@example.com",
                "user@exam_ple.com",
                "user@domain.com"
            };

            foreach (var email in testEmails)
            {
                bool isValid = emailValidator.Validate(email);
                Console.WriteLine($"Email: {email}, Valid: {isValid}");
            }
        }
    }
}
