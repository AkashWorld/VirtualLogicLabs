using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInsert : MonoBehaviour
{
    public string[] students;
    public static string studentName;
    public static string passWord;
    public static int Lab1Grade;
    public static int Lab2Grade;
    WWW studentData;
    public string gradeOwner;
    string CreateUserURL = "http://ec2-18-204-3-220.compute-1.amazonaws.com/ReadData.php";
    StudentInfo currStudent;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(GetData());
        currStudent = new StudentInfo();
        StudentInfo.ReadFromFile(currStudent);
    }

    public void InsertStudentGrade(string student, string password, int lab1grade, int lab2grade)
    {
        WWWForm form = new WWWForm();
        form.AddField("studentPost", student);
        form.AddField("passwordPost", password);
        form.AddField("lab1gradePost", lab1grade);
        form.AddField("lab2gradePost", lab2grade);
        WWW www = new WWW(CreateUserURL, form);
    }

    public IEnumerator GetData()
    {
        studentData = new WWW("http://ec2-18-204-3-220.compute-1.amazonaws.com/ReadData.php");
        yield return studentData;
        string studentDataString = studentData.text;
        print(studentDataString);
        students = studentDataString.Split(';');
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

    public int GetStudentLab1Grade(string name)
    {
        string grade = "";

        for (int i = 0; i < students.Length; i++)
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
        Lab1Grade = finalGrade;
        return finalGrade;
    }

    public int GetStudentLab2Grade(string name)
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
        Lab2Grade = finalGrade;
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
        passWord = password;
        return password;
    }

    public void setGradeOwner(string owner)
    {
        this.gradeOwner = owner;
    }
    
}