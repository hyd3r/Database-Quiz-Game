
[System.Serializable]
public class score
{

    public string user;
    public int scored;
    public string date;

    public score(string user, int scored, string date)
    {
        this.user = user;
        this.scored = scored;
        this.date = date;
    }

}
