
//Librerias del ADO .NET
using System.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using Lab03Consola;

class Program
{
    // Cadena de conexión a la base de datos
    public static string connectionString = "Data Source=Lab1504-11\\SQLEXPRESS;Initial Catalog=Tecsup2023DB;User ID=Tecsup00;Password=123456";


    static void Main()
    {
       



        #region FormaConectada
        //Datareader
        List<Student> estudiantes = ListarEstudiantesListaObjetos();
        foreach (var item in estudiantes)
        {
            Console.WriteLine($"StudentsID: {item.StudentsId}, FirstName: {item.FirstName}, LastName: {item.LastName}");
        }
        #endregion


    }

    //De forma desconectada
    private static DataTable ListarEstudiantesDataTable()
    {
        // Crear un DataTable para almacenar los resultados
        DataTable dataTable = new DataTable();
        // Crear una conexión a la base de datos
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT * FROM Empleados";

            // Crear un adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);



            // Llenar el DataTable con los datos de la consulta
            adapter.Fill(dataTable);

            // Cerrar la conexión
            connection.Close();

        }
        return dataTable;
    }
    //De forma conectada
    private static List<Student> ListarEstudiantesListaObjetos()
    {
        List<Student> estudiantes = new List<Student>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT StudentsID,FirstName,LastName FROM Students";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Verificar si hay filas
                    if (reader.HasRows)
                    {
                        Console.WriteLine("Lista de Estudiantes:");
                        while (reader.Read())
                        {
                            // Leer los datos de cada fila

                            estudiantes.Add(new Student
                            {
                                StudentsId = (int)reader["StudentsID"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()
                            });

                        }
                    }
                }
            }

            // Cerrar la conexión
            connection.Close();


        }
        return estudiantes;

    }


}
