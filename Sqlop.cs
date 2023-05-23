using Microsoft.Data.Sqlite;
using System.Globalization;
class Sqlop{
    

    public bool DataAdd(productitem item){      //データ追加
        bool a = true;
        try{
            using (var connection = new SqliteConnection("Data Source=product.db"))
            {
                connection.Open(); //データーベース接続


                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS product(id TEXT NOT NULL PRIMARY KEY, "+ //テーブル作成
                        "name TEXT," +
                        "Price INTEGER," +
                        "Genre TEXT," +
                        "Description TEXT," +
                        "Url TEXT," +
                        "Date TEXT)";

                    cmd.ExecuteNonQuery();



                    cmd.CommandText = "INSERT INTO product VALUES "+ //データベースに追加
                    $"('{item.id}','{item.Name}','{item.Price}','{item.Genre}','{item.Description}','{item.Url}','{item.Date}')";

                    cmd.ExecuteNonQuery();
                    
                    cmd.CommandText = "SELECT * FROM product";

                    using (var reader = cmd.ExecuteReader())
                    {
                        
                        var dump = reader.Read();

                        Console.WriteLine(reader["name"]);
                    }
             };
            return a;
        }
        catch{
            a =false;
            return a;
        }
            

        }

        public productitem[] DBDataGet(){        //データ読み出し
            try{
                List<productitem> list = new List<productitem>();

                using (var connection = new SqliteConnection("Data Source=product.db"))
                {
                    connection.Open(); //データーベース接続
                    
                    var cmd = connection.CreateCommand();

                    string query =  "SELECT * FROM product";    

                    cmd.CommandText = query;

                    using (var reader = cmd.ExecuteReader()){
                        
                        while (reader.Read() == true){      // DBのデータをディクショナリに変換
                            var item = new productitem();
                            item.id = (string)reader["id"];
                            item.Name = (string)reader["name"];
                            var Price = (long)reader["Price"];
                            item.Genre = (string)reader["Genre"];
                            item.Description = (string)reader["Description"];
                            item.Url = (string)reader["Url"];
                            DateTime time;
                            DateTime.TryParse((string)reader["Date"],out time);
                            item.Date = time;

                            var Pricest = string.Format(CultureInfo.InvariantCulture, "{0:0,0}", Price );
                            item.Price = Pricest+"円";
            
                            list.Add(item);

                        }
                    }
                    productitem[] rt_list = list.ToArray();
                    return rt_list;
                    }

                }
                catch{
                    productitem[] rt_list = null;
                    return rt_list;
                }
        }


        public string DBDataGetPriceSum(){ //金額合計計算
            long sumPrice = 0;
            string sumPriceString = "";
            try{
                using (var connection = new SqliteConnection("Data Source=product.db"))
                {
                    connection.Open(); //データーベース接続
                    
                    var cmd = connection.CreateCommand();

                    string query = "SELECT SUM(Price) AS sumPrice From product ";

                    cmd.CommandText = query;

                    using(var reader = cmd.ExecuteReader()){
                        if(reader.Read() == true){
                            sumPrice = (long)reader["sumPrice"];
                            sumPriceString = string.Format(CultureInfo.InvariantCulture, "{0:0,0}", sumPrice );
                            sumPriceString =  sumPriceString +"円";
                        }
                    }
                }
            }
            catch{
                sumPriceString = "NoData";
                return sumPriceString;
            }

            return sumPriceString;

        }


        public string DBDataGetlistSum(){ //総リスト数
            long list = 0;
            string ListCount = "";
            try{
                using (var connection = new SqliteConnection("Data Source=product.db"))
                {
                    connection.Open(); //データーベース接続
                    
                    var cmd = connection.CreateCommand();

                    string query = "SELECT COUNT(*) AS ListCount From product ";

                    cmd.CommandText = query;

                    using(var reader = cmd.ExecuteReader()){
                        if(reader.Read() == true){
                            list = (long)reader["ListCount"];
                            ListCount = string.Format(CultureInfo.InvariantCulture, "{0:0,0}", list );
                        }
                    }
                }
            }
            catch{
                ListCount = "NoData";
                return ListCount;
            }

            return ListCount;

        }


    public void DelDB(string[] id){ //DB行削除

        using (var connection = new SqliteConnection("Data Source=product.db"))
        {
            connection.Open(); //データーベース接続
                
            var cmd = connection.CreateCommand();

            foreach(string i in id){

                cmd.CommandText = $"DELETE FROM product WHERE id = '{i}'";

                cmd.ExecuteNonQuery();

            }


        }
    

    }  
}



