namespace API_Tiktok.Modelo
{
    public class Usuario
    {
        public string idUsuario { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string rol { get; set; }

        public static List<Usuario> DB()
        {
            var List = new List<Usuario>()
            {
                new Usuario{
                    idUsuario="1",
                    usuario="Jorge",
                    password="123",
                    rol="empleado"
                },
                new Usuario{
                    idUsuario="2",
                    usuario="Luis",
                    password="123",
                    rol="administrador"
                },
            };

            return List;
        }


    }
}
