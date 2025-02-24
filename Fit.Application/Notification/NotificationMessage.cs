namespace Fit.Application.Notification;

public class NotificationMessage
{
    public static class Common
    {
        public static readonly string UnexpectedError = "Erro inesperado!";
        public static readonly string ValidationError = "Ocorreram um ou mais erros de validação!";
        public static readonly string RequestListRequired = "Lista da requisição não pode estar vazia!";
        public static readonly string DataExists = "Dados já cadastrados";
    }

    public static class Workout
    {
        public static readonly string OnlyPersonal = "Apenas personals podem prescrever treino!";
        public static readonly string NotFound = "Treino não encontrado!(Apenas o criador do treino tem permissão para altera-lo)";
    }

    public static class User
    {
        public static readonly string PasswordAreDifferent = "As senhas são diferentes!";
        public static readonly string EmailAlreadyExists = "Esse email já foi cadastrado!";
        public static readonly string InvalidData = "Dados inválidos!";
        public static readonly string InvalidToken = "Token inválido!";
        public static readonly string UserNameAlreadyExists = "Esse nome de usuário já foi cadastrado!";
        public static readonly string NotFound = "Usuário não encontrado!";
    }

    public static class Exercise
    {
        public static readonly string NotFound = "Exercício não encontrado!";
        public static readonly string MissingIds = "É necessário passas todos os exercicios para ordena-los!";
    }
}
