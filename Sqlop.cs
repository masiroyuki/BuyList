using Microsoft.Data.Sqlite;
using System.Globalization;
class Sqlop{
    

    public void DataAdd(productitem item){      //データ追加

            using (var connection = new SqliteConnection("Data Source=product.db"))
            {
                connection.Open(); //データーベース接続


                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS product(id TEXT NOT NULL PRIMARY KEY, "+ //テーブル作成
                        "name TEXT," +
                        "Price TEXT," +
                        "Genre TEXT," +
                        "Description TEXT," +
                        "Url TEXT," +
                        "Date TEXT)";

                    cmd.ExecuteNonQuery();


                    var Price = string.Format(CultureInfo.InvariantCulture, "{0:0,0}", int.Parse(item.Price) );
                    Price = Price+"円";


                    cmd.CommandText = "INSERT INTO product VALUES "+ //データベースに追加
                    $"('{item.id}','{item.Name}','{Price}','{item.Genre}','{item.Description}','{item.Url}','{item.Date}')";

                    cmd.ExecuteNonQuery();
                    
                    cmd.CommandText = "SELECT * FROM product";

                    using (var reader = cmd.ExecuteReader())
                    {
                        
                        var dump = reader.Read();

                        Console.WriteLine(reader["name"]);
                    }
             };
        }

        public productitem[] DBDataGet(){        //データ読み出し

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
                        item.Price = (string)reader["Price"];
                        item.Genre = (string)reader["Genre"];
                        item.Description = (string)reader["Description"];
                        item.Url = (string)reader["Url"];
                        DateTime time;
                        DateTime.TryParse((string)reader["Date"],out time);
                        item.Date = time;
        
                        list.Add(item);

                        Console.WriteLine(item.Date);
                    }
                }
                productitem[] rt_list = list.ToArray();
                return rt_list;

            }     
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



