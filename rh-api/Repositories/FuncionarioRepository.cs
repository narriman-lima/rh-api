using rh_api.Enums;
using rh_api.Models;

namespace rh_api.Repositories
{
    public static class FuncionarioRepository
    {
        public static readonly List<FuncionarioModel> _funcionarios = new List<FuncionarioModel>()
        {
            new FuncionarioModel
            {
                Id = 1,
                Nome = "Narriman",
                Senha = "123",
                Salario = 5000,
                Permissao = Enums.PermissoesEnum.Administrador
            }
        };

        private static int _id = 1;
        public static List<FuncionarioModel> ListarFuncionarios()
        {
            return _funcionarios;
        }

        public static FuncionarioModel ObterPorUsuarioESenha(string usuario, string senha)
        {
            return _funcionarios.FirstOrDefault(x => x.Nome.ToLower() == usuario.ToLower() && x.Senha == senha);
        }

        public static void AdicionarFuncionario(string nome, string senha, decimal salario, PermissoesEnum permissao)
        {
            _funcionarios.Add(new FuncionarioModel
            {
                Id = _id++,
                Nome = nome,
                Senha = senha,
                Salario = salario,
                Permissao = permissao
            }); 
        }

        public static void AdicionarFuncionario(FuncionarioModel geradorDeId)
        {
            geradorDeId.Id = _id++;
            _funcionarios.Add(geradorDeId);
        }

        public static void EditarFuncionario(FuncionarioModel geradorDeId, int id)
        {
            FuncionarioModel funcionario = _funcionarios.Find(x => x.Id == id);
            funcionario.Nome = geradorDeId.Nome;
            funcionario.Senha = geradorDeId.Senha;
            funcionario.Salario = geradorDeId.Salario;
            funcionario.Permissao = geradorDeId.Permissao;
        }

        public static void DeletarFuncionario(FuncionarioModel geradorDeId)
        {
            _funcionarios.Remove(geradorDeId);
        }
    }
}
