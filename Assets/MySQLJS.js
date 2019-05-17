    // In truth, the only things you want to save to the database are dynamic objects
    // static objects in the scene will always exist, so make sure to set your Tag 
    // based on the documentation for this demo

    // values to match the database columns
    var ID;
    var Name;
    var levelname;
    var objectType;
    var posx;
    var posy;
    var posz;
    var tranx;
    var trany;
    var tranz;
  
    var saving = false;
    var loading = false;

    // MySQL instance specific items
    var constr = "Server=localhost;Database=demo;User ID=demo;Password=demo;Pooling=true";
    // connection object
    var con;
    // command object
    var cmd;
    // reader object
    var rdr;
    // object collection array
    var bodies:GameObject[];
    // object definitions
    class data
    {
        var UID;
        var ID;
        var Name;
        var levelname;
        var objectType;
        var posx;
        var posy;
        var posz;
        var tranx;
        var trany;
        var tranz;
      }
    // collection container
    var _GameItems;
    function Awake()
    {
        try
        {
            // setup the connection element
            con = new MySql.Data.MySqlClient.MySqlConnection(constr);

            // lets see if we can open the connection
            con.Open();
            Debug.Log("Connection State: " + con.State);
        } 
        catch (ex)
        {
            Debug.Log(ex.ToString());
        }

    }

    function OnApplicationQuit()
    {
        Debug.Log("killing con");
        if (con != null)
        {
            con.Close();
            con.Dispose();
        }
    }

    // Use this for initialization
    function Start()
    {

    }

    // Update is called once per frame
    function Update()
    {

    }


    // gui event like a button, etc
    function OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 50, 30), "Save") && !saving)
        {
            saving = true;
            // first lets clean out the databae
            DeleteEntries();
            // now lets save the scene information
            InsertEntries();
            // you could also use the update if you know the ID of the item already saved

            saving = false;
        }
        if (GUI.Button(new Rect(10, 110, 50, 30), "Load") && !loading)
        {
            loading = true;
            // lets read the items from the database
            ReadEntries();
            // now display what is known about them to our log
            LogGameItems();
            loading = false;
        }
    }

    // Insert new entries into the table
    function InsertEntries()
    {
        prepData();
        var query;
        // Error trapping in the simplest form
        try
        {
            query = "INSERT INTO demo_table (ID, Name, levelname, objectType, posx, posy, posz, tranx, trany, tranz) VALUES (?ID, ?Name, ?levelname, ?objectType, ?posx, ?posy, ?posz, ?tranx, ?trany, ?tranz)";
            if (con.State.ToString() != "Open")
                con.Open();
            
                for (itm in _GameItems)
                {
                        var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, con);
                        var oParam = cmd.Parameters.AddWithValue("?ID", itm.ID);
                        var oParam1 = cmd.Parameters.AddWithValue("?Name", itm.Name);
                        var oParam2 = cmd.Parameters.AddWithValue("?levelname", itm.levelname);
                        var oParam3 = cmd.Parameters.AddWithValue("?objectType", itm.objectType);
                        var oParam4 = cmd.Parameters.AddWithValue("?posx", itm.posx);
                        var oParam5 = cmd.Parameters.AddWithValue("?posy", itm.posy);
                        var oParam6 = cmd.Parameters.AddWithValue("?posz", itm.posz);
                        var oParam7 = cmd.Parameters.AddWithValue("?tranx", itm.tranx);
                        var oParam8 = cmd.Parameters.AddWithValue("?trany", itm.trany);
                        var oParam9 = cmd.Parameters.AddWithValue("?tranz", itm.tranz);
                        cmd.ExecuteNonQuery();
                    }
        }
        catch (ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    // Update existing entries in the table based on the iddemo_table
    function UpdateEntries()
    {
        prepData();
        var query;
        // Error trapping in the simplest form
        try
        {
            query = "UPDATE demo_table SET ID=?ID, Name=?Name, levelname=?levelname, objectType=?objectType, posx=?posx, posy=?posy, posz=?posz, tranx=?tranx, trany=?trany, tranz=?tranz WHERE iddemo_table=?UID";
            if (con.State.ToString() != "Open")
                con.Open();
            
                for(itm in _GameItems)
                {
                        var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, con);
                        var oParam = cmd.Parameters.AddWithValue("?ID", itm.ID);
                        var oParam1 = cmd.Parameters.AddWithValue("?Name", itm.Name);
                        var oParam2 = cmd.Parameters.AddWithValue("?levelname", itm.levelname);
                        var oParam3 = cmd.Parameters.AddWithValue("?objectType", itm.objectType);
                        var oParam4 = cmd.Parameters.AddWithValue("?posx", itm.posx);
                        var oParam5 = cmd.Parameters.AddWithValue("?posy", itm.posy);
                        var oParam6 = cmd.Parameters.AddWithValue("?posz", itm.posz);
                        var oParam7 = cmd.Parameters.AddWithValue("?tranx", itm.tranx);
                        var oParam8 = cmd.Parameters.AddWithValue("?trany", itm.trany);
                        var oParam9 = cmd.Parameters.AddWithValue("?tranz", itm.tranz);
                        var oParam10 = cmd.Parameters.AddWithValue("?UID", itm.UID);
                        cmd.ExecuteNonQuery();
                }      
        }
        catch (ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    // Delete entries from the table
    function DeleteEntries()
    {
        var query;
        // Error trapping in the simplest form
        try
        {
            // optimally you will know which items you want to delete from the database
            // using the following code and the record ID, you can delete the entry
            //-----------------------------------------------------------------------
            // query = "DELETE FROM demo_table WHERE iddemo_table=?UID";
            // MySqlParameter oParam = cmd.Parameters.Add("?UID", MySqlDbType.Int32);
            // oParam.Value = 0;
            //-----------------------------------------------------------------------
            query = "DELETE FROM demo_table WHERE iddemo_table";
            if (con.State.ToString() != "Open")
                con.Open();
 
                var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
        }
        catch (ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    // Read all entries from the table
    function ReadEntries()
    {
        var query;
        if (_GameItems == null)
            _GameItems = new Array();
        if (_GameItems.Count > 0)
            _GameItems.Clear();
        // Error trapping in the simplest form
        try
        {
            query = "SELECT * FROM view_demo";
            if (con.State.ToString() != "Open")
                con.Open();
                 var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, con);
                
                    rdr = cmd.ExecuteReader();
                    if(rdr.HasRows)
                    while (rdr.Read())
                    {
                        var itm = new data();
                        itm.UID = int.Parse(rdr["iddemo_table"].ToString());
                        itm.ID = rdr["ID"].ToString();
                        itm.levelname = rdr["levelname"].ToString();
                        itm.Name = rdr["Name"].ToString();
                        itm.objectType = rdr["objectType"].ToString();
                        itm.posx = float.Parse(rdr["posx"].ToString());
                        itm.posy = float.Parse(rdr["posy"].ToString());
                        itm.posz = float.Parse(rdr["posz"].ToString());
                        itm.tranx = float.Parse(rdr["tranx"].ToString());
                        itm.trany = float.Parse(rdr["trany"].ToString());
                        itm.tranz = float.Parse(rdr["tranz"].ToString());
                        _GameItems.Add(itm);
                    }
                    rdr.Dispose();
        }
        catch (ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    /// <summary>
    /// Lets show what was read back to the log window
    /// </summary>
    function LogGameItems()
    {
        if (_GameItems != null)
        {
            if (_GameItems.Count > 0)
            {
                for(itm in _GameItems)
                {
                    Debug.Log("UID: " + itm.UID);
                    Debug.Log("ID: " + itm.ID);
                    Debug.Log("levelname: " + itm.levelname);
                    Debug.Log("Name: " + itm.Name);
                    Debug.Log("objectType: " + itm.objectType);
                    Debug.Log("posx: " + itm.posx);
                    Debug.Log("posy: " + itm.posy);
                    Debug.Log("posz: " + itm.posz);
                    Debug.Log("tranx: " + itm.tranx);
                    Debug.Log("trany: " + itm.trany);
                    Debug.Log("tranz: " + itm.tranz);
                }
            }
        }
    }

    /// <summary>
    /// This method prepares the data to be saved into our database
    /// 
    /// </summary>
    function prepData()
    {
        bodies = GameObject.FindGameObjectsWithTag("Savable");
        _GameItems = new Array();
        var itm;
        for (body in bodies)
        {
            itm = new data();
            itm.ID = body.name + "_" + body.GetInstanceID();
            itm.Name = body.name;
            itm.levelname = Application.loadedLevelName;
            itm.objectType = body.name.Replace("(Clone)", "");
            itm.posx = body.transform.position.x;
            itm.posy = body.transform.position.y;
            itm.posz = body.transform.position.z;
            itm.tranx = body.transform.rotation.x;
            itm.trany = body.transform.rotation.y;
            itm.tranz = body.transform.rotation.z;
            _GameItems.Add(itm);
        }
        Debug.Log("Items in collection: " + _GameItems.Count);
    }