
[System.Serializable]
public class question {

    public int questionID;
	public string fact;
	public bool isTrue;
    public string details;

    public question(int questionID, string fact, bool isTrue, string details)
    {
        this.questionID = questionID;
        this.fact = fact;
        this.isTrue = isTrue;
        this.details = details;
    }
    public question(string fact)
    {
        this.fact = fact;
    }
    public question(bool isTrue)
    {
        this.isTrue = isTrue;
    }
}
