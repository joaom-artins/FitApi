namespace Fit.Application.Notification;

public class NotificationMessage
{
    public static class Common
    {
        public static readonly string UnexpectedError = "Erro inesperado!";
        public static readonly string ValidationError = "Ocorreram um ou mais erros de validação!";
        public static readonly string RequestListRequired = "Lista da requisição não pode estar vazia!";
    }

    public static class User
    {
        public static readonly string PasswordAreDifferent = "As senhas são diferentes!";
        public static readonly string EmailAlreadyExists = "Esse email já foi cadastrado!";
        public static readonly string UserNameAlreadyExists = "Esse nome de usuário já foi cadastrado!";
    }
}
