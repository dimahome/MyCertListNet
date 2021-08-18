using System;
using System.Security.Cryptography.X509Certificates;

namespace MyCertListNet
{
    class Program
    {
        static bool boolIssuerOut = false;
        static bool boolSubjectOut = false;
        static bool boolNotAfterOut = false;
        static bool boolNotBeforeOut = false;
        static bool boolPrintLine = false;
        static string stFilterIssuer = "";
        static string stFilterSubject = "";

        static bool ReadArgs(string[] args)
        {

            foreach (string arg in args)
            {
                string argUpper = arg.ToUpper();

                if (argUpper.Contains("/?"))
                {
                    return false;
                }
                else if (argUpper.Contains("/P"))
                {
                    boolPrintLine = true;
                }
                else if (argUpper.Contains("/A"))
                {
                    boolNotAfterOut = true;
                }
                else if (argUpper.Contains("/B"))
                {
                    boolNotBeforeOut = true;
                }
                else if (argUpper.Contains("/I"))
                {
                    boolIssuerOut = true;
                    if (argUpper.Length > 3)
                    {
                        if (argUpper[2].Equals(":"[0]))
                        {
                            stFilterIssuer = argUpper.Substring(3);
                        }
                        else
                        {
                            Console.WriteLine("Invalid key:  " + argUpper);
                            return false;
                        };

                    }
                    else if (!(argUpper.Length == 2))
                    {
                        return false;
                    }
                }
                else if (argUpper.Contains("/S"))
                {
                    boolSubjectOut = true;
                    if (argUpper.Length > 3)
                    {
                        if (argUpper[2].Equals(":"[0]))
                        {
                            stFilterSubject = argUpper.Substring(3);
                        }
                        else
                        {
                            Console.WriteLine("Invalid key:  " + argUpper);
                            return false;
                        };

                    }
                    else if (!(argUpper.Length == 2))
                    {
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid key:  " + argUpper);
                    return false;
                }


                //Console.WriteLine(arg);
            }
            return true;

        }

        static void PrintHelp()
        {
            Console.WriteLine("Prints to the console a list of installed personal certificates.");
            Console.WriteLine("MyCertLis.exe [/I[:FIELDS]] [/S[:FIELDS]] [/A] [/B] [/L]");
            Console.WriteLine("");
            Console.WriteLine("/I    Issuer data printing.");
            Console.WriteLine("      You can specify a filter for fields issuer output to the console. For example \"/I:CN,OU\"");
            Console.WriteLine("/S    Subject data printing.");
            Console.WriteLine("      You can specify a filter for fields subject output to the console. For example \"/S:SN,STREET\"");
            Console.WriteLine("/A    NotAfter date time printing.");
            Console.WriteLine("/B    NotBefore date time printing.");
            Console.WriteLine("/P    Printing a dividing line (empty line).");
            Console.WriteLine("/?    Printing this help.");
            Console.WriteLine("Running without key prints the maximum available information to the console.");
        }


        static void Main(string[] args)
        {

            //for test replase args to argstest
            string[] argstest = { "/P", "/I:CN,SN,OU", "/s:ИНН", "/a" };


            if (args.Length > 0)
            {
                bool compliteReadArgs = ReadArgs(args);
                if (!compliteReadArgs)
                {
                    PrintHelp();
                    Environment.Exit(0);

                };

            }
            else
            {
                boolIssuerOut = true;
                boolSubjectOut = true;
                boolNotAfterOut = true;
                boolNotBeforeOut = true;
                boolPrintLine = true;
            }

            ReadMyCert();

            //Console.ReadLine();
        }

        static void ReadMyCert()
        {
            //BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open),Encoding.Unicode);
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);

            store.Open(OpenFlags.ReadOnly);

            foreach (X509Certificate2 certificate in store.Certificates)
            {
                if (boolPrintLine)
                {
                    Console.WriteLine("");
                };

                if (boolIssuerOut)
                {
                    string[] filtersIsArrayStr = stFilterIssuer.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] issuerArrayStr = certificate.Issuer.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); 
                    if (filtersIsArrayStr.Length == 0)
                    {
                        Console.WriteLine("Issuer: " + certificate.Issuer);
                    }
                    else
                    {
                        Console.Write("Issuer: ");
                        string outConsoleStr = "";
                        foreach (string issuerStr in issuerArrayStr)
                        {
                            foreach (string filterStr in filtersIsArrayStr)
                            {

                                if (issuerStr.Trim().Substring(0, filterStr.Length + 1) == filterStr + "=")
                                {
                                     if (outConsoleStr.Length > 0)
                                    {
                                        outConsoleStr = outConsoleStr + ", ";
                                    }

                                    outConsoleStr = outConsoleStr + issuerStr.Trim();
                                }
                            }
                        };
                        Console.WriteLine(outConsoleStr);
                    }
                    filtersIsArrayStr = null;
                    issuerArrayStr = null;

                };
                if (boolSubjectOut)
                {

                    string[] filtersSuArrayStr = stFilterSubject.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] subjectArrayStr = certificate.Subject.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (filtersSuArrayStr.Length == 0)
                    {
                        Console.WriteLine("Subject: " + certificate.Subject);
                    }
                    else
                    {
                        Console.Write("Subject: ");
                        string outConsoleStr = "";
                        foreach (string subjectStr in subjectArrayStr)
                        {
                            foreach (string filterStr in filtersSuArrayStr)
                            {
                                if (subjectStr.Trim().Substring(0, filterStr.Length + 1) == filterStr + "=")
                                {
                                    if (outConsoleStr.Length > 0)
                                    {
                                        outConsoleStr = outConsoleStr + ", ";
                                    }

                                    outConsoleStr = outConsoleStr + subjectStr;
                                }
                            }

                        };
                        Console.WriteLine(outConsoleStr);
                    }
                    filtersSuArrayStr = null;
                    subjectArrayStr = null;

                };
                if (boolNotAfterOut)
                {
                    Console.WriteLine("NotAfter: " + certificate.NotAfter.ToString());
                };
                if (boolNotBeforeOut)
                {
                    Console.WriteLine("NotBefore: " + certificate.NotBefore.ToString());
                };

            }
            store.Close();

        }

    }
}

