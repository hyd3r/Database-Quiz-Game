
[System.Serializable]
public class account
{

    public int user_ID;
    public string username;
    public string password;

    public account(int user_ID, string username, string password)
    {
        this.user_ID = user_ID;
        this.username = username;
        this.password = password;
    }

}
