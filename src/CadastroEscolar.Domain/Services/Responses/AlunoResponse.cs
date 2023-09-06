namespace CadastroEscolar.Domain.Services.Responses;

public class AlunoResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public decimal Mensalidade { get; set; }
    public DateTime DataVencimento { get; set; }
    public string NomeProfessor { get; set; }
}