using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pit2Api.Infra.Repositories
{
    public static class Scripts
    {
        public const string GetAllWeatherForecasts = @"
            SELECT Date, TemperatureC, TemperatureF, Summary
            FROM WeatherForecast
        ";

        public const string NickNameExists = @"
SELECT CASE WHEN EXISTS(SELECT 1 FROM Usuario WHERE NickName = @NickName) THEN 1 ELSE 0 END
";

        public const string InsertUsuario = @"
INSERT INTO Usuario (Id, NickName, Senha, IdPerguntaSeguranca, RespostaPergunta)
VALUES (@Id, @NickName, HASHBYTES('SHA2_512', @Senha), @IdPerguntaSeguranca, @RespostaPergunta)
";

        public const string GetSecurityQuestionByNick = @"
SELECT p.Descricao
FROM Usuario u
INNER JOIN PerguntaSeguranca p ON p.Id = u.IdPerguntaSeguranca
WHERE u.NickName = @NickName
";

        public const string ValidateLogin = @"
SELECT CASE WHEN EXISTS(
    SELECT 1 FROM Usuario
    WHERE NickName = @NickName
      AND Senha = HASHBYTES('SHA2_512', @Senha)
) THEN 1 ELSE 0 END
";

        public const string ValidateSecurityAnswer = @"
SELECT CASE WHEN EXISTS(
    SELECT 1 FROM Usuario
    WHERE NickName = @NickName
      AND LOWER(RTRIM(LTRIM(RespostaPergunta))) = LOWER(RTRIM(LTRIM(@Resposta)))
) THEN 1 ELSE 0 END
";

        public const string ChangePassword = @"
UPDATE Usuario
SET Senha = HASHBYTES('SHA2_512', @Senha)
WHERE NickName = @NickName
";

        public const string GetAllSecurityQuestions = @"
SELECT Id, Descricao FROM PerguntaSeguranca ORDER BY Id
";

        // Game related scripts
        public const string GetAllComplexidades = @"
SELECT Id, Descricao
FROM Complexidade
ORDER BY Id
";

        public const string InsertJogo = @"
INSERT INTO Jogo (Id, IdUsuario, IdComplexidade, Nome, DuracaoMinutos, QtdMinimaJogadores, QtdMaximaJogadores, IdadeMinima)
VALUES (@Id, @IdUsuario, @IdComplexidade, @Nome, @DuracaoMinutos, @QtdMinimaJogadores, @QtdMaximaJogadores, @IdadeMinima)
";

        public const string UpdateJogo = @"
UPDATE Jogo
SET IdComplexidade = @IdComplexidade,
    Nome = @Nome,
    DuracaoMinutos = @DuracaoMinutos,
    QtdMinimaJogadores = @QtdMinimaJogadores,
    QtdMaximaJogadores = @QtdMaximaJogadores,
    IdadeMinima = @IdadeMinima
WHERE Id = @Id
";

        public const string DeleteJogo = @"
DELETE FROM Jogo
WHERE Id = @Id
";
    }
}
