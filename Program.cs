using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace MultiThreadedSearch
{
    class Program
    {
        //Define a Static List:
        static List<Tuple<int, int>> list = new List<Tuple<int, int>>();
        //Define a Static Flage:
        static bool flg = false;
        //
        static string[] TextLines;
        //
        static Regex rx_For_Pre_Processing;
        //
        static Regex rx_For_Matching;
        //
        static int CandidateStringLen;
        static void Main(string[] args)
        {

            /******************** Get Input from the user ********************/
            Console.WriteLine("Enter a text File Address");
            string TextFileAdress = Console.ReadLine();
            //2.String to Search:
            Console.WriteLine("Enter a string keyword to search");
            string StringToSearch = Console.ReadLine();
            //3.Number of threads
            Console.WriteLine("Enter the Number of threads");
            int nThreads = int.Parse(Console.ReadLine());
            //4.The Delta number
            Console.WriteLine("Enter the Delta");
            int Delta = int.Parse(Console.ReadLine());

            //for example:
            //string TextFileAdress = @"C:\Users\עומר\source\repos\Ex2_Question4\Ex2_Question4\Text Files for example\Alice.txt";
            //2.String to Search:
            //string StringToSearch = "*";
            //3.Number of threads
            //int nThreads = 5;
            //4.The Delta number
            //int Delta = 9;


            //Pre-Processing:
            //Step A: Calculate the two regex:
            //Step A_1: Calculate the  Regex to Identify the Candidates Strings Using the Calculate_Regex_For_Candidate Function:
            rx_For_Pre_Processing = new Regex(Calculate_Regex_For_Candidate(StringToSearch));
            //Step A_2: Calculate the the Regex Who Identify the matches String:
            Tuple<string, int> tup = Calculate_Regex_For_Match(StringToSearch, Delta);
            string StringToSearch_Phrase_Pattern = tup.Item1;
            rx_For_Matching = new Regex(StringToSearch_Phrase_Pattern);
            //Step B: Calculate the Leanght of the string:
            CandidateStringLen = tup.Item2;

            //Step c:Read the file.txt to a array
            TextLines = File.ReadAllLines(TextFileAdress, Encoding.UTF8);


            //Part A for the Use in thread-Pre processing Using nThreads

            int N = 0;
            int M = TextLines.Length;
            int Therad_responsible = (M - N) / nThreads;
            Thread[] arr = new Thread[nThreads];
            int index_start = N;
            int index_end = index_start + Therad_responsible;

            for (int i = 0; i < nThreads; i++)
            {
                arr[i] = new Thread(() => Program.Pre_Processing_WorkerFunction(index_start, index_end));
                arr[i].Start();
                Thread.Sleep(70);
                if (i == nThreads - 2)
                {
                    index_start = index_end;
                    index_end = M;
                }
                else
                {
                    index_start = index_end;
                    index_end = index_end + Therad_responsible;
                }
            }

            for (int j = 0; j < nThreads; j++)
            {
                arr[j].Join();
            }
            List<Tuple<int, int>> mico = list;
            //Part B for the Use in thread for search the Matching
            N = 0;
            M = list.Count;
            Therad_responsible = (M - N) / nThreads;
            arr = new Thread[nThreads];
            index_start = N;
            index_end = index_start + Therad_responsible;

            for (int i = 0; i < nThreads; i++)
            {
                arr[i] = new Thread(() => Program.WorkerFunction(index_start, index_end));
                arr[i].Start();
                Thread.Sleep(70);
                if (i == nThreads - 2)
                {
                    index_start = index_end;
                    index_end = M;
                }
                else
                {
                    index_start = index_end;
                    index_end = index_end + Therad_responsible;
                }
            }

            for (int j = 0; j < nThreads; j++)
            {
                arr[j].Join();
            }

            //Check if nothing dont print to the screen:
            if (flg == false)
            {
                Console.WriteLine("not found");
            }

        }
        /******************** Initalizing the Helper Function to the Program********************/
        /**
          *1.Calculate_Regex_For_Candidate(string StringToSearch) 
          * Input:
          *     -(string) StringToSearch:the function get the pharse that need to serach
          *    
          * Output:
          *     -(string) Identify_Candidate_Pattern: the function will return the regex who should help the candidate string pharse.
          *      
          * For example:From the next input: StringToSearch='HL' =>we  get the next Output:regex-[H]
          */
        public static string Calculate_Regex_For_Candidate(string StringToSearch)
        {
            string StringToSearch_First_char = StringToSearch[0].ToString();
            String Identify_Candidate_Pattern = @"[" + StringToSearch_First_char + "]";
            return Identify_Candidate_Pattern;
        }
        /**
         * 2.Calculate_Regex_For_Match(string StringToSearch, int Delta)
          * Input:
          *     -(string) StringToSearch:the function get the pharse that need to serach
          *     -(int) Delta: the function get the delta between each char in the string
          * 
          * Output:
          *     -(string) StringToSearch_Phrase_Pattern: the function will return the regex who represent the candidate string-the regex build according
          *                                             the next two input from the user: StringToSearch,Delta.
          *     -(int) CandidateStringLen: the function will return the Leangth of the Match string  
          *      
          * For example:From the next input: StringToSearch='HL',Delta=2 =>we  get the next Output:regex-[H]..[L],CandidateStringLen=4
          */
        public static Tuple<string, int> Calculate_Regex_For_Match(string StringToSearch, int Delta)
        {
            string DeltaInRgex = "";
            for (int i = 0; i < Delta; i++)
            {
                DeltaInRgex += ".";
            }
            String StringToSearch_Phrase_Pattern = @"";
            int Len_StringToSearch = StringToSearch.Length;
            int CandidateStringLen = 0;
            if (Len_StringToSearch <= 1)
            {
                StringToSearch_Phrase_Pattern += "[" + StringToSearch[0].ToString() + "]";
                CandidateStringLen = Len_StringToSearch;
            }
            else
            {
                CandidateStringLen = Len_StringToSearch + Delta * (Len_StringToSearch - 1);
                for (int i = 0; i < Len_StringToSearch - 1; i++)
                {
                    StringToSearch_Phrase_Pattern += "[" + StringToSearch[i].ToString() + "]";
                    StringToSearch_Phrase_Pattern += DeltaInRgex;
                }
                StringToSearch_Phrase_Pattern += "[" + StringToSearch[Len_StringToSearch - 1].ToString() + "]";
            }

            return Tuple.Create(StringToSearch_Phrase_Pattern, CandidateStringLen);
        }

        /**
         *3.Pre_Processing_WorkerFunction(string[] TextLines, Regex rx_For_Pre_Processing)
         * Input:
         *     -(string[]) TextLines: the functon get an array that each cell represent a row from the text file.
         *     -(Regex) rx_For_Pre_Processing: the function get a regex that its role is to find the candidate strings.
         *    
         * Output:
         *     -(List<Tuple<int, int>>) list: the function will return a list of tupple that each cell represent the location
         *                                    of a candidate string in the text file on the next order:
         *                                    Tuple(row_number,position in the row)
         *      
         * For example:For the next input: TextLines='[hello word,....]',rx_For_Pre_Processing = '[w]' >we will get the next Output:[tup(0,6),......]
         */
        public static void Pre_Processing_WorkerFunction(int Start_Search, int EndSearch)
        {
            //Define the index of line:
            string line = "";
            for (int k = Start_Search; k < EndSearch; k++)
            {
                line = TextLines[k];
                // Find matches.
                MatchCollection Pre_Processing_matches = rx_For_Pre_Processing.Matches(line);
                // Report on each match.
                foreach (Match match in Pre_Processing_matches)
                {
                    GroupCollection groups = match.Groups;
                    list.Add(Tuple.Create(k, groups[0].Index));
                }
            }

        }
        /**
        *4.WorkerFunction(string[] TextLines, List<Tuple<int, int>> list, Regex rx_For_Matching, int CandidateStringLen)
        * Input:
        *     -(string[]) TextLines: the functon get an array that each cell represent a row from the text file.
        *     -(List<Tuple<int, int>>) list: the function get  list of tupple that each cell represent the location
        *                                   of a candidate string in the text file on the next order:
        *                                   Tuple(row_number,position in the row)
        *     -(Regex) rx_For_Matching: the function get a regex that its role is to find the string in the text who match.
        *     -(int) CandidateStringLen: the function get an leangth of the string that had to match
        *    
        * Output:
        *     -if the candidate string has a match the function will print his propertys-
        *     other whise the function will dont print nothing!
        *                                   
        */
        public static void WorkerFunction(int Start_Search, int EndSearch)
        {
            String Candidate_String = "";
            Tuple<int, int> tuple_row_position;
            int Row_number;
            int Position_number;

            for (int t = Start_Search; t < EndSearch; t++)
            {
                Candidate_String = "";
                //get the tupple who represent the candidate string location
                tuple_row_position = list[t];
                //Get the row number and the position of the candidate string-from the tuple:
                Row_number = tuple_row_position.Item1;
                Position_number = tuple_row_position.Item2;
                Candidate_String += TextLines[Row_number].Substring(Position_number);
                int count_HowManyRowsToAdd = 1;
                while ((Candidate_String.Length < CandidateStringLen) && (Row_number + count_HowManyRowsToAdd < TextLines.Length))
                {
                    Candidate_String += " " + TextLines[Row_number + count_HowManyRowsToAdd];
                    count_HowManyRowsToAdd++;
                }
                MatchCollection matches = rx_For_Matching.Matches(Candidate_String);
                foreach (Match match in matches)
                {
                    //Check if the match who found is prefer to the candidate string 
                    GroupCollection groups = match.Groups;
                    if (groups[0].Index == 0)
                    {
                        flg = true;
                        Console.WriteLine("[" + Row_number + "," + Position_number + "]");
                    }
                }
            }

        }

    }
}
