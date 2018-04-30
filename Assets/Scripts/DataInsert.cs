using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInsert : MonoBehaviour {

    public string inputStudent;
    public string inputPassword;
    public int inputGrade;
    public string[] students;
    WWW studentData;

    string CreateUserURL = "http://ec2-18-204-3-220.compute-1.amazonaws.com/ReadData.php";

    // Use this for initialization

    public void InsertStudentGrade(string student, string password, int grade)
    {
        WWWForm form = new WWWForm();
        form.AddField("studentPost", student);
        form.AddField("passwordPost", password);
        form.AddField("gradePost", grade);

        WWW www = new WWW(CreateUserURL, form);
    }

    public IEnumerator GetData()
    {
        studentData = new WWW("http://ec2-18-204-3-220.compute-1.amazonaws.com/ReadData.php");
        yield return studentData;

        string studentDataString = studentData.text;
        print(studentDataString);

        students = studentDataString.Split(';');

        print(GetStudentGrade("Dhruvik"));
        print(GetStudentPassword("Ethan"));
    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }

    int GetStudentGrade(string name)
    {
        string grade = "";

        for (int i = 0; i < students.Length; i++)
        {
            string value = GetDataValue(students[i], "Student:");

            if (value.Equals(name, StringComparison.Ordinal))
            {
                grade = GetDataValue(students[i], "Grade:");
                break;
            }
        }

        int finalGrade;
        finalGrade = Int32.Parse(grade);
        return finalGrade;
    }

    string GetStudentPassword(string name)
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
}
