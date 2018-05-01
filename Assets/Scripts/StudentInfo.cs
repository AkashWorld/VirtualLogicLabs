using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;

public class StudentInfo{
    public string Name;
    public string Password;
    public int lab1Grade;
    public int lab2Grade;

    public StudentInfo()
    {
        Name = "";
        Password = "";
        lab1Grade = 0;
        lab2Grade = 0;
    }
    public static void WriteToFile(StudentInfo currStudent)
    {
        string[] lines = { currStudent.Name, currStudent.Password, "" + currStudent.lab1Grade, "" + currStudent.lab2Grade };
        System.IO.File.WriteAllLines(Directory.GetCurrentDirectory() + "\\currStudent.txt", lines);
    }

    public static void ReadFromFile(StudentInfo currStudent)
    {
        string[] lines = System.IO.File.ReadAllLines(Directory.GetCurrentDirectory() + "\\currStudent.txt");
        for(int i = 0; i < 4; i++)
        {
            if(i == 0)
            {
                currStudent.Name = lines[i];
            }
            else if(i == 1)
            {
                currStudent.Password = lines[i];
            }
            else if(i == 2)
            {
                currStudent.lab1Grade = Int32.Parse(lines[i]);
            }
            else if(i == 3)
            {
                currStudent.lab2Grade = Int32.Parse(lines[i]);
            }
        }
    }


}
