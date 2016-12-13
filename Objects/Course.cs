using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Registrar.Objects
{
  public class Course
  {
    private int _id;
    private string _name;
    private string _number;

    public Course(string name, string number, int id=0)
    {
      _id = id;
      _name = name;
      _number = number;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetNumber()
    {
      return _number;
    }

    public override bool Equals(Object otherCourse)
    {
      if(!(otherCourse is Course))
      {
        return false;
      }
      else
      {
        Course newCourse = (Course) otherCourse;
        bool idEquality = (this._id == newCourse.GetId());
        bool nameEquality = (this._name == newCourse.GetName());
        bool numberEquality = (this._number == newCourse.GetNumber());
        return (idEquality && nameEquality && numberEquality);
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM courses", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static List<Course> GetAll()
    {
      List<Course> allCourses= new List<Course>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string number = rdr.GetString(2);

        Course newCourse = new Course(name, number, id);
        allCourses.Add(newCourse);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allCourses;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO courses (name, number) OUTPUT INSERTED.id VALUES (@Name, @Number);", conn);

      cmd.Parameters.AddWithValue("@Name", _name);
      cmd.Parameters.AddWithValue("@Number", _number);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        _id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public void UpdateName(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand ("UPDATE courses SET name = @NewName OUTPUT INSERTED.name WHERE id = @CourseId;", conn);

      cmd.Parameters.AddWithValue("@NewName", newName);
      cmd.Parameters.AddWithValue("@CourseId", _id);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Course Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses WHERE id = @CourseId;", conn);
      cmd.Parameters.AddWithValue("@CourseId", id);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string name = "";
      string number = "";

      while(rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        name = rdr.GetString(1);
        number = rdr.GetString(2);
      }
      Course foundCourse = new Course(name, number, foundId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCourse;
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM courses WHERE id = @CourseId;", conn);
      cmd.Parameters.AddWithValue("@CourseId", _id);
      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
