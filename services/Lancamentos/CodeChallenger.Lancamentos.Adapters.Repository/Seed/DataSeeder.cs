namespace CodeChallenger.Lancamentos.Adapters.Repository.Seed
{
    using CodeChallenger.Lancamentos.Domain.Encryption;
    using CodeChallenger.Lancamentos.Domain.Entity;
    using CodeChallenger.Lancamentos.Domain.Repository;

    public class DataSeeder
    {
        public DataSeeder(IWriteRepository writeRepository, ISha512Service shaService)
        {
            _writeRepository = writeRepository;
            _shaService = shaService;
        }

        private readonly IWriteRepository _writeRepository;
        private readonly ISha512Service _shaService;

        public async Task SeedData()
        {
            var senha = _shaService.Hash("teste");

            var userGerente = new Usuario()
                .SetNome("Gerente")
                .SetEmail("gerente@teste.com")
                .SetPerfisAcesso([Roles.GERENTE])
                .SetSenha(senha);

            var userAtendente = new Usuario()
                .SetNome("Atendente")
                .SetEmail("atendente@teste.com")
                .SetPerfisAcesso([Roles.ATENDENTE])
                .SetSenha(senha);

            await _writeRepository.SaveOrUpdateAsync([userGerente, userAtendente]);
        }
    }
}
