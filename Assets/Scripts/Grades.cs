using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grades : MonoBehaviour {
    string gradeOwner;
    List<Grade> GradesList;
    // Use this for initialization
    void Start() {
        GradesList = new List<Grade>();
    }

    public void setGradeOwner(string owner)
    {
        this.gradeOwner = owner;
    }

    public void putGrade(string gradeKey, double gradeValue)
    {
        foreach(Grade grade in GradesList)
        {
            if(grade.getGradeKey() == gradeKey)
            {
                grade.setGradeValue(gradeValue);
                return;
            }
        }
        Grade newgrade = new Grade(gradeKey, gradeValue);
        this.GradesList.Add(newgrade);
    }

    public void removeGrade(string gradeKey)
    {
        foreach(Grade grade in GradesList)
        {
            if(grade.getGradeKey() == gradeKey)
            {
                GradesList.Remove(grade);
                return;
            }
        }
    }
    // Update is called once per frame
    void Update() {

    }


    public class Grade {
        private string gradeKey;
        private double gradeValue;

        public Grade(string key, double val)
        {
            this.gradeKey = key;
            this.gradeValue = val;
        }

        public string getGradeKey()
        {
            return gradeKey;
        }
        public double getGradeValue()
        {
            return gradeValue;
        }
        public void setGradeKey(string newKey)
        {
            this.gradeKey = newKey;
        }
        public void setGradeValue(double newVal)
        {
            this.gradeValue = newVal;
        }
    }
}
