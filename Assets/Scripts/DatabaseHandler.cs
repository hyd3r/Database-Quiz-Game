using UnityEngine;
using System;
using System.Data;
using System.Text;

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

using MySql.Data;
using MySql.Data.MySqlClient;

public class DatabaseHandler : MonoBehaviour {
	public string host, database, user, password;
	public bool pooling = true;

	private string connectionString;
	private MySqlConnection con = null;
	private MySqlCommand cmd = null;
	private MySqlDataReader rdr = null;

	private MD5 _md5Hash;

    void Awake(){
		
		connectionString = "Server=" + host + ";Database=" + database + ";User=" + user + ";Password=" + password + ";CharSet=utf8;Pooling=";
		if (pooling) {
			connectionString += "True";
		} else {
			connectionString += "False";
		}
        query("SELECT * FROM users");
    }
	void onApplicationQuit(){
		if (con != null) {
			if (con.State.ToString () != "Closed") {
				con.Close ();
				Debug.Log ("Mysql connection closed");
			}
			con.Dispose ();
		}
	}

    public int getRowCount()
    {
        int numRows = 0;
        query("SELECT * FROM questions");
        using (rdr = cmd.ExecuteReader())
        {
            while (rdr.Read())
            {
                numRows++;
            }
        }
        return numRows;
    }

    public void query(string query)
    {
        try
        {
            con = new MySqlConnection(connectionString);
            con.Open();

            string sql = query;
            cmd = new MySqlCommand(sql, con);

        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

	public question[] getQuestions(){
        int rowIndex = 0;
        int id;
        string fact;
        bool ans;
        string detail;

        query("SELECT * FROM questions");

        question[] questions=new question[getRowCount()];
		using (rdr = cmd.ExecuteReader ()) {
			while (rdr.Read ()) {
                id = Int32.Parse(rdr[0].ToString());
                fact = rdr[1].ToString();
                if (Int32.Parse(rdr[2].ToString()) == 1)
                {
                    ans = true;
                }
                else
                {
                    ans = false;
                }
                detail = rdr[3].ToString();
                questions[rowIndex] = new question(id,fact, ans,detail);
                rowIndex++;
			}
		}
		return questions;
	}
	public string GetConnectionState(){
		return con.State.ToString ();
	}

    public string addUser(string user,string pass)
    {
        string status="";
        if (user.Equals("") || pass.Equals(""))
        {
            status = "Don't leave any fields blank";
        }
        else
        {
            query("SELECT * FROM users where username='" + user + "'");
            using (rdr = cmd.ExecuteReader())
            {
                if (rdr.Read())
                {
                    status = "Username already exists";
                }
                else
                {
                    query("INSERT INTO users (username, password) VALUES ('" + user + "', '" + pass + "')");
                    cmd.ExecuteNonQuery();
                    status = user+" successfully added";
                }
            }
        }
        return status;
    }

    public List<account> displayUsers()
    {
        List<account> users=new List<account>();
        query("SELECT * FROM users");
        using (rdr = cmd.ExecuteReader())
        {
            while (rdr.Read())
            {
                users.Add(new account(Int32.Parse(rdr[0].ToString()), rdr[1].ToString(), rdr[2].ToString()));
            }
        }
        return users;
    }

    public void addScore()
    {
          query("INSERT INTO scores (user_id,game_ID,score,dateTime) VALUES ('" + PlayerPrefs.GetInt("userid") + "', '" + PlayerPrefs.GetInt("gameid") + "', '" + PlayerPrefs.GetInt("score") + "', '" + DateTime.Now.ToString("yyy-MM-dd HH:mm:ss") + "')");
          cmd.ExecuteNonQuery();
    }

    public List<score>[] displayScores()
    {
        List<score>[] scores = new List<score>[2];
        scores[0]= new List<score>();
        scores[1] = new List<score>();
        query("SELECT * FROM scores inner join users on scores.user_id=users.user_id order by score desc");
        using (rdr = cmd.ExecuteReader())
        {
            while (rdr.Read())
            {
                switch (rdr[2])
                {
                    case 1:
                        scores[0].Add(new score(rdr[6].ToString(),Int32.Parse(rdr[3].ToString()),rdr[4].ToString()));
                        break;
                    case 2:
                        scores[1].Add(new score(rdr[6].ToString(), Int32.Parse(rdr[3].ToString()), rdr[4].ToString()));
                        break;
                }
                
            }
        }
        return scores;
    }
}
