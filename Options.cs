using CommandLine;

namespace PuttyWrapper
{
    public class Options
    {
        [Option('n', "new", HelpText = "Creates a new Putty configuration through an interactive prompt")]
        public bool New { get; set; }
        [Option('d', "delete", HelpText = "Deletes a saved Putty configuration")]
        public bool Delete { get; set; }
        [Option('c', "connect", HelpText = "Connects to a saved Putty configuration")]
        public bool Connect { get; set; }
        [Option('p', "print", HelpText = "Prints the directory for the saved configurations, as well as a list of them")]
        public bool Print { get; set; }

        [Value(0, HelpText = "The name of the saved Putty configuration", Required = false, MetaName = "Name")]
        public string Name { get; set; }

        [Value(1, HelpText = "The user for a new Putty configuration", Required = false, MetaName = "User")]
        public string User { get; set; }
        [Value(2, HelpText = "The remote IP address/domain for a new Putty configuration", Required = false, MetaName = "IP Address")]
        public string IpAddress { get; set; }
        [Value(3, HelpText = "The password for the user in a new Putty configuration", Required = false, MetaName = "Password")]
        public string Password { get; set; }
    }
}