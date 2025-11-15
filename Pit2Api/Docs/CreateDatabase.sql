CREATE TABLE PerguntaSeguranca (
    Id int PRIMARY KEY,
    Descricao varchar(100)
);

INSERT INTO PerguntaSeguranca VALUES (1, 'Qual o seu número de telefone (com DDD)?')

CREATE TABLE Complexidade (
    Id int PRIMARY KEY,
    Descricao varchar(100)
);

INSERT INTO Complexidade VALUES (1, 'Jogos Simples e fáceis de aprender (Ex.: Uno)')
INSERT INTO Complexidade VALUES (2, 'Jogos com mais regras, porém ainda são de fácil compreensão (Ex.: War)')
INSERT INTO Complexidade VALUES (3, 'Jogos bastante complexos. Ideais para quem AMA BOARD GAMES :) (Ex.: Terra Mystica)')

CREATE TABLE Usuario (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    NickName varchar(100) UNIQUE,
    Senha varbinary(max),
    IdPerguntaSeguranca int,
    RespostaPergunta varchar(100)

    FOREIGN KEY(IdPerguntaSeguranca) REFERENCES PerguntaSeguranca(Id)
);

Create Table Jogo (
  Id uniqueidentifier PRIMARY KEY,
  IdUsuario uniqueidentifier,
  IdComplexidade int,
  Nome varchar(100),
  DuracaoMinutos int,
  QtdMinimaJogadores int,
  QtdMaximaJogadores int,
  IdadeMinima int,

  FOREIGN KEY(IdUsuario) REFERENCES Usuario(Id),
  FOREIGN KEY(IdComplexidade) REFERENCES Complexidade(Id)
)

create Table Secao (
  Id uniqueidentifier PRIMARY KEY,
  IdUsuario uniqueidentifier,
  IdadeJogadorMaisNovo int,
  DuracaoMinutos int,
  QtdJogadores int,
  NivelComplexidadeMinima int,
  NivelComplexidadeMaxima int,

  FOREIGN KEY(IdUsuario) REFERENCES Usuario(Id),
    FOREIGN KEY(NivelComplexidadeMinima) REFERENCES Complexidade(Id),
    FOREIGN KEY(NivelComplexidadeMaxima) REFERENCES Complexidade(Id)

)

CREATE Table JogosSecao (
  IdSecao uniqueidentifier UNIQUE,
  IdJogo uniqueidentifier UNIQUE,
  PrimeiraVez bit,
  FOREIGN KEY(IdSecao) REFERENCES Secao(Id),
  FOREIGN KEY(IdJogo) REFERENCES Jogo(Id),
)