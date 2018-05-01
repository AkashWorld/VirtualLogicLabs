using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataInsert : MonoBehaviour {
    public static string inputStudent;
    public static string inputPassword;
    public static int inputLab1Grade;
    public static int inputLab2Grade;
    public static double lab1avg;
    public static double lab2avg;
    public string[] students;
    WWW studentData;
    public string gradeOwner;
    string CreateUserURL = "http://ec2-18-204-3-220.compute-1.amazonaws.com/ReadData.php";
    StudentInfo currStudent;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(GetData());
   
    }


    public void InsertStudentGrade(string student, string password, int lab1grade, int lab2grade)
    {
        WWWForm form = new WWWForm();
        form.AddField("studentPost", student);
        form.AddField("passwordPost", password);
        form.AddField("lab1gradePost", lab1grade);
        form.AddField("lab2gradePost", lab2grade);
        WWW www = new WWW(CreateUserURL, form);
        StartCoroutine(GetData());
    }

    public IEnumerator GetData()
    {
        studentData = new WWW("http://ec2-18-204-3-220.compute-1.amazonaws.com/ReadData.php");
        yield return studentData;
        string studentDataString = studentData.text;
        print(studentDataString);
        students = studentDataString.Split(';');
        lab1avg = getlab1Avg();
        lab2avg = getlab2avg();
        yield break;
    }

    public string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }

    public void SetAllValues(string name)
    {
        inputStudent = name;
        inputLab1Grade = GetStudentLab1Grade(name);
        inputLab2Grade = GetStudentLab2Grade(name);
        inputPassword = GetStudentPassword(name);
    }


    public bool CheckIfStudentExists(string name)
    {
        Debug.Log("Student array length: " + students.Length);
        for(int i = 0; i < students.Length - 1; i++)
        {
            string studentName = GetDataValue(students[i], "Student:");
            Debug.Log("Student name: " + studentName);
            if (studentName == name)
            {
                Debug.Log("Student found!");
                SetAllValues(name);
                return true;
            }
        }
        return false;
    }


    int GetStudentLab1Grade(string name)
    {
        string grade = "";

        for (int i = 0; i < students.Length - 1; i++)
        {
            
            string value = GetDataValue(students[i], "Student:");

            if (value.Equals(name, StringComparison.Ordinal))
            {
                grade = GetDataValue(students[i], "Lab 1 Grade:");
                break;
            }
        }

        int finalGrade;
        finalGrade = Int32.Parse(grade);
        Debug.Log("Setting current user Lab 1 Grade to: " + inputLab1Grade);
        return finalGrade;
    }

    public double getlab1Avg()
    {
        double avg = 0;
        double total = 0;
        for (int i = 0; i < students.Length - 1; i++)
        {
            string studentName = GetDataValue(students[i], "Student:");
            total += GetStudentLab1Grade(studentName);
        }
        avg = total / (students.Length - 1);
        return avg;
    }

    public double getlab2avg()
    {
        double avg = 0;
        double total = 0;
        for (int i = 0; i < students.Length - 1; i++)
        {
            string studentName = GetDataValue(students[i], "Student:");
            total += GetStudentLab2Grade(studentName);
        }
        avg = total / (students.Length - 1);
        return avg;
    }

    int GetStudentLab2Grade(string name)
    {
        string grade = "";

        for (int i = 0; i < students.Length; i++)
        {
            string value = GetDataValue(students[i], "Student:");
        if (value.Equals(name, StringComparison.Ordinal))
            {
                grade = GetDataValue(students[i], "Lab 2 Grade:");
                break;
            }
        }
        int finalGrade;
        finalGrade = Int32.Parse(grade);
        Debug.Log("Setting current user Lab 2 Grade to: " + inputLab2Grade);
        return finalGrade;
    }

    public string GetStudentPassword(string name)
    {
        string password = "";

        for (int i = 0; i < students.Length; i++)
        {
            string value = GetDataValue(students[i], "Student:");

            if (value.Equals(name, StringComparison.Ordinal))
            {
                password = GetDataValue(students[i], "Password:");
                break;
            }
        }
        return password;
    }

    public void setGradeOwner(string owner)
    {
        this.gradeOwner = owner;
    }
    

    public void PrintToCSVFormat()
    {

    }

}
