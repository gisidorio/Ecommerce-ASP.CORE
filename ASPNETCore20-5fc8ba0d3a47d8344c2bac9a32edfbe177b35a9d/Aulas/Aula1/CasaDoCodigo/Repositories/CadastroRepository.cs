using CasaDoCodigo.Models;

namespace CasaDoCodigo.Repositories
{
    public class CadastroRepository : BaseRepository<Cadastro>, ICadastroRepository
    {
        public CadastroRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
