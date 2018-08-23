using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Moodle_User_Checker
{
    

    class Program
    {

        static void Main(string[] args)
        {

            List<string> list_moodle_username = new List<string>();
            StreamReader moodle_rdr = new StreamReader(".\\users_from_moodle.csv");
            string moodle_line = moodle_rdr.ReadLine(); //skip header line
            moodle_line = moodle_rdr.ReadLine(); //read 1st data line
            while (moodle_line != null)
            {
                string[] moodle_substrings = moodle_line.Split(',');
                list_moodle_username.Add(moodle_substrings[1]);

                moodle_line = moodle_rdr.ReadLine();
            }
           

            StreamWriter writer = new StreamWriter(".\\output.txt");


            StreamReader file_rdr = new StreamReader(".\\sftp_down_time.csv");
            string line = file_rdr.ReadLine(); //skip header line
            line = file_rdr.ReadLine(); //read 1st data line
            while (line != null)
            {
                string[] substrings = line.Split(',');

                string people_code_id = substrings[1];
                string username = substrings[2];

                if (list_moodle_username.Contains(username))
                    writer.WriteLine("***** FOUND_IN_MOODLE people_code_id={0} username={1}", people_code_id, username);
                else
                    writer.WriteLine("NOT FOUND IN MODDLE people_code_id={0} username={1}", people_code_id, username);
                        
                line = file_rdr.ReadLine();
            }//while

            file_rdr.Close();
            writer.Close();

            Console.WriteLine("DONE");
        }//main
    }//program
}//namespace


