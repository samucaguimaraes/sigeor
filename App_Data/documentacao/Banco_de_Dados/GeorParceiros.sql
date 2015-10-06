--create database dbSebraeParceiro;
use dbSebraeParceiro;

CREATE TABLE t01_entidade (
  t01_cd_entidade INT NOT NULL IDENTITY(1, 1),
  nm_entidade VARCHAR(500) NULL,
  nm_uf VARCHAR(100) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_ativa BIT NULL,
  PRIMARY KEY(t01_cd_entidade)
);

CREATE TABLE t02_usuario (
  t02_cd_usuario VARCHAR(20) NOT NULL,
  t01_cd_entidade INT NULL,
  t05_cd_parceiro INT NULL,
  nm_nome VARCHAR(200) NULL,
  nm_cargo VARCHAR(100) NULL,
  nm_email VARCHAR(100) NULL,
  nu_telefone BIGINT NULL,
  nu_celular BIGINT NULL,
  pw_senha NVARCHAR(50) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_ativa BIT NULL,
  PRIMARY KEY(t02_cd_usuario)
);

--select * from  t02_usuario
--delete from  t22_LOG

INSERT INTO t02_usuario 
VALUES('adm', 0,0, 'Suporte ADM', 
'Super Administrador', 'suporte@macroplan.com.br', null, null, 
PWDENCRYPT('2'), getdate(), getdate(), 1); 

--select * from t01_entidade
--select * from t02_usuario

--select * from t02_usuario where t01_cd_entidade 01_cd_entidade=3 or t01_cd_entidade in 
--(select t01_cd_entidade from t05_parceiro where t01_cd_entidade=3)) order by nm_nome

--select * from t03_projeto
--delete from t03_projeto
--select * from dbo.t22_log

CREATE TABLE t03_projeto (
  t03_cd_projeto INT NOT NULL IDENTITY(1, 1),
  t02_cd_usuario VARCHAR(20) NULL,
  t02_cd_usuario_monitoramento VARCHAR(20) NULL,
  t04_cd_tipologia INT NULL,
  t01_cd_entidade INT NULL,
  nm_projeto VARCHAR(500) NULL,
  ds_publico TEXT NULL,
  ds_objetivo TEXT NULL,
  dt_inicio DATETIME NULL,
  dt_fim DATETIME NULL,
  dt_acordo DATETIME NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_ativa BIT NULL,
  PRIMARY KEY(t03_cd_projeto)
);

CREATE TABLE t04_tipologia (
  t04_cd_tipologia INT NOT NULL IDENTITY(1, 1),
  nm_tipologia VARCHAR(500) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_ativa BIT NULL,
  PRIMARY KEY(t04_cd_tipologia)
);

CREATE TABLE t05_parceiro (
  t05_cd_parceiro INT NOT NULL IDENTITY(1, 1),
  t01_cd_entidade INT NULL,
  nm_parceiro VARCHAR(500) NULL,
  nm_arquivo VARCHAR(500) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t05_cd_parceiro)
);

CREATE TABLE t06_parceiroprojeto (
  t06_cd_parceiroprojeto INT NOT NULL IDENTITY(1, 1),
  t03_cd_projeto INT NULL,
  t05_cd_parceiro INT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t06_cd_parceiroprojeto)
);

CREATE TABLE t07_restricao (
  t07_cd_restricao INT NOT NULL IDENTITY(1, 1),
  t03_cd_projeto INT NULL,
  ds_restricao TEXT NULL,
  ds_medida TEXT NULL,
  ds_providencia TEXT NULL,
  dt_superada DATETIME NULL,
  dt_limite DATETIME NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_ativa BIT NULL,
  PRIMARY KEY(t07_cd_restricao)
);

CREATE TABLE t08_acao (
  t08_cd_acao INT NOT NULL IDENTITY(1, 1),
  t03_cd_projeto INT NULL,
  t02_cd_usuario VARCHAR(20) NULL,
  nm_acao VARCHAR(500) NULL,
  ds_acao TEXT NULL,
  dt_inicio DATETIME NULL,
  dt_fim DATETIME NULL,
  dt_original DATETIME NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_ativa BIT NULL,
  PRIMARY KEY(t08_cd_acao)
);

CREATE TABLE t09_marco (
  t09_cd_marco INT NOT NULL IDENTITY(1, 1),
  t08_cd_acao INT NULL,
  t02_cd_usuario VARCHAR(20) NULL,
  ds_marco TEXT NULL,
  nu_esforco INT NULL,
  dt_prevista DATETIME NULL,
  dt_realizada DATETIME NULL,
  dt_original DATETIME NULL,
  ds_comentario TEXT NULL,
  fl_status CHAR(1) NULL,
  nu_ordem INT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_ativa BIT NULL,
  PRIMARY KEY(t09_cd_marco)
);

--select * from t09_marco 
CREATE TABLE t10_produto (
  t10_cd_produto INT NOT NULL IDENTITY(1, 1),
  t08_cd_acao INT NULL,
  ds_produto TEXT NULL,
  nu_ano INT NULL,
  nm_medida VARCHAR(50) NULL,
  vl_previsto DECIMAL(18,2) NULL,
  vl_realizado DECIMAL(18,2) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t10_cd_produto)
);

CREATE TABLE t11_financeiro (
  t11_cd_financeiro INT NOT NULL IDENTITY(1, 1),
  t08_cd_acao INT NULL,
  t05_cd_parceiro INT NULL,
  nm_entidade VARCHAR(200) NULL,
  nu_ano INT NULL,
  vl_previsto DECIMAL(18,2) NULL,
  vl_realizado DECIMAL(18,2) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t11_cd_financeiro)
);

CREATE TABLE t12_premissa (
  t12_cd_premissa INT NOT NULL IDENTITY(1, 1),
  t03_cd_projeto INT NULL,
  nm_premissa VARCHAR(500) NULL,
  nu_ordem INT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_ativa BIT NULL,
  PRIMARY KEY(t12_cd_premissa)
);

CREATE TABLE t13_foco (
  t13_cd_foco INT NOT NULL IDENTITY(1, 1),
  t03_cd_projeto INT NULL,
  nm_foco VARCHAR(500) NULL,
  nu_ordem INT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_ativa BIT NULL,
  PRIMARY KEY(t13_cd_foco)
);

CREATE TABLE t14_resultado (
  t14_cd_resultado INT NOT NULL IDENTITY(1, 1),
  t03_cd_projeto INT NULL,
  nm_resultado VARCHAR(500) NULL,
  ds_resultado TEXT NULL,
  nm_medida VARCHAR(200) NULL,
  nu_ano INT NULL,
  vl_t0 DECIMAL(18,2) NULL,
  fl_acumulado BIT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_ativa BIT NULL,
  nm_unid VARCHAR(200),
  PRIMARY KEY(t14_cd_resultado)
);

CREATE TABLE t15_vlresultado (
  t15_cd_vlresultado INT NOT NULL IDENTITY(1, 1),
  t14_cd_resultado INT NULL,
  nu_ano INT NULL,
  vl_previsto DECIMAL(18,2) NULL,
  vl_realizado DECIMAL(18,2) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t15_cd_vlresultado)
);

CREATE TABLE t16_situacao (
  t16_cd_situacao INT NOT NULL IDENTITY(1, 1),
  t03_cd_projeto INT NULL,
  ds_situacao TEXT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t16_cd_situacao)
);

CREATE TABLE t17_colaborador (
  t17_cd_colaborador INT NOT NULL IDENTITY(1, 1),
  t03_cd_projeto INT NULL,
  t02_cd_usuario VARCHAR(20) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t17_cd_colaborador)
);

CREATE TABLE t18_documento (
  t18_cd_documento INT NOT NULL IDENTITY(1, 1),
  t03_cd_projeto INT NULL,
  nm_documento VARCHAR(1000) NULL,
  ds_descricao TEXT NULL,
  nm_arquivo VARCHAR(500) NULL,
  fl_foto BIT NULL,
  fl_video BIT NULL,
  fl_cronograma BIT NULL,
  fl_outros BIT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_ativa BIT NULL,
  PRIMARY KEY(t18_cd_documento)
);

CREATE TABLE t19_fase (
  t19_cd_fase INT NOT NULL IDENTITY(1, 1),
  nm_fase VARCHAR(200) NULL,
  ds_fase TEXT NULL,
  fl_fase CHAR(2) NULL,
  nm_arquivo VARCHAR(200) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_ativa BIT NULL,
  PRIMARY KEY(t19_cd_fase)
);

insert into t19_fase values('Em Estruturação', '', 'ES', '', getdate(), getdate(),1);
insert into t19_fase values('Em Execução', '', 'EX', '', getdate(), getdate(),1);
insert into t19_fase values('Em Revisão', '', 'RE', '', getdate(), getdate(),1);
insert into t19_fase values('Concluído', '', 'CO', '', getdate(), getdate(),1);
insert into t19_fase values('Encerrado', '', 'EN', '', getdate(), getdate(),1);

--SELECT * FROM t19_fase
select * from t20_faseprojeto where fl_ativa=1 and t19_cd_fase in (select t19_cd_fase from t19_fase where fl_fase='ES')
--select * from t19_fase t19, t20_faseprojeto t20 where t19.t19_cd_fase = t20.t19_cd_fase and  t20.fl_ativa=1 and t19.fl_fase='ES'

CREATE TABLE t20_faseprojeto (
  t20_cd_faseprojeto INT NOT NULL IDENTITY(1, 1),
  t03_cd_projeto INT NULL,
  t19_cd_fase INT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_ativa BIT NULL,
  PRIMARY KEY(t20_cd_faseprojeto)
);
--select * from t20_faseprojeto
--update t20_faseprojeto set fl_ativa=1

CREATE TABLE t21_acesso (
  t21_cd_acesso INT NOT NULL IDENTITY(1, 1),
  t02_cd_usuario VARCHAR(20) NULL,
  nm_ip VARCHAR(100) NULL,
  dt_data DATETIME NULL,
  PRIMARY KEY(t21_cd_acesso)
);

CREATE TABLE t22_log (
  t22_cd_log INT NOT NULL IDENTITY(1, 1),
  t02_cd_usuario VARCHAR(20) NULL,
  t03_cd_projeto INT NULL,
  ds_log TEXT NULL,
  nm_tabela VARCHAR(200) NULL,
  nm_comando VARCHAR(200) NULL,
  nm_valor VARCHAR(200) NULL,
  dt_data DATETIME NULL,
  PRIMARY KEY(t22_cd_log)
);

CREATE TABLE t23_noticia (
  t23_cd_noticia INT NOT NULL IDENTITY(1, 1),
  t03_cd_projeto INT NULL,
  nm_noticia VARCHAR(500) NULL,
  ds_noticia TEXT NULL,
  dt_data DATETIME NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_ativa BIT NULL,
  PRIMARY KEY(t23_cd_noticia)
);

CREATE TABLE t24_agenda (
  t24_cd_agenda INT NOT NULL IDENTITY(1, 1),
  t03_cd_projeto INT NULL,
  nm_agenda VARCHAR(500) NULL,
  ds_agenda TEXT NULL,
  dt_data DATETIME NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_ativa BIT NULL,
  PRIMARY KEY(t24_cd_agenda)
);

CREATE TABLE t25_perfil (
  t25_cd_perfil INT NOT NULL IDENTITY(1, 1),
  nm_perfil VARCHAR(500) NULL,
  ds_perfil TEXT NULL,
  nm_cod VARCHAR(10) NULL,
  nu_ordem INT NULL,
  ds_comentario TEXT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t25_cd_perfil)
);


insert into t25_perfil values('Administrador Geral', 'fl_admin', 'AG', 1,'Administra todos os usuários, tipologias e entidades', getdate(), getdate());
insert into t25_perfil values('Administrador Parceiro', 'fl_adminparceiro', 'AP', 2,'Administra projetos e usuários de sua entidade', getdate(), getdate());
insert into t25_perfil values('Estratégico', 'fl_estrategico', 'ES', 3,'Acesso restrito a todos os projetos', getdate(), getdate());

--select * from t25_perfil

CREATE TABLE t26_usuarioperfil (
  t26_cd_usuarioperfil INT NOT NULL IDENTITY(1, 1),
  t02_cd_usuario VARCHAR(20) NOT NULL,
  t25_cd_perfil INT NOT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t26_cd_usuarioperfil)
);

--select * from t26_usuarioperfil

select * from dbo.t18_documento order by dt_cadastro desc

-- INÍCIO NOVAS TABELAS

CREATE TABLE t27_vlproduto (
  t27_cd_vlproduto INT NOT NULL IDENTITY(1, 1),
  t10_cd_produto INT  NULL,
  nu_ano INT NULL,
  vl_p1 DECIMAL(18,2) NULL,
  vl_p2 DECIMAL(18,2) NULL,
  vl_p3 DECIMAL(18,2) NULL,
  vl_p4 DECIMAL(18,2) NULL,
  vl_p5 DECIMAL(18,2) NULL,
  vl_p6 DECIMAL(18,2) NULL,
  vl_p7 DECIMAL(18,2) NULL,
  vl_p8 DECIMAL(18,2) NULL,
  vl_p9 DECIMAL(18,2) NULL,
  vl_p10 DECIMAL(18,2) NULL,
  vl_p11 DECIMAL(18,2) NULL,
  vl_p12 DECIMAL(18,2) NULL,
  vl_r1 DECIMAL(18,2) NULL,
  vl_r2 DECIMAL(18,2) NULL,
  vl_r3 DECIMAL(18,2) NULL,
  vl_r4 DECIMAL(18,2) NULL,
  vl_r5 DECIMAL(18,2) NULL,
  vl_r6 DECIMAL(18,2) NULL,
  vl_r7 DECIMAL(18,2) NULL,
  vl_r8 DECIMAL(18,2) NULL,
  vl_r9 DECIMAL(18,2) NULL,
  vl_r10 DECIMAL(18,2) NULL,
  vl_r11 DECIMAL(18,2) NULL,
  vl_r12 DECIMAL(18,2) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t27_cd_vlproduto)
);

CREATE TABLE t28_vlfinanceiro (
  t28_cd_vlfinanceiro INT NOT NULL IDENTITY(1, 1),
  t11_cd_financeiro INT NULL,
  nu_ano INT NULL,
  vl_p1 DECIMAL(18,2) NULL,
  vl_p2 DECIMAL(18,2) NULL,
  vl_p3 DECIMAL(18,2) NULL,
  vl_p4 DECIMAL(18,2) NULL,
  vl_p5 DECIMAL(18,2) NULL,
  vl_p6 DECIMAL(18,2) NULL,
  vl_p7 DECIMAL(18,2) NULL,
  vl_p8 DECIMAL(18,2) NULL,
  vl_p9 DECIMAL(18,2) NULL,
  vl_p10 DECIMAL(18,2) NULL,
  vl_p11 DECIMAL(18,2) NULL,
  vl_p12 DECIMAL(18,2) NULL,
  vl_r1 DECIMAL(18,2) NULL,
  vl_r2 DECIMAL(18,2) NULL,
  vl_r3 DECIMAL(18,2) NULL,
  vl_r4 DECIMAL(18,2) NULL,
  vl_r5 DECIMAL(18,2) NULL,
  vl_r6 DECIMAL(18,2) NULL,
  vl_r7 DECIMAL(18,2) NULL,
  vl_r8 DECIMAL(18,2) NULL,
  vl_r9 DECIMAL(18,2) NULL,
  vl_r10 DECIMAL(18,2) NULL,
  vl_r11 DECIMAL(18,2) NULL,
  vl_r12 DECIMAL(18,2) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t28_cd_vlfinanceiro)
);

CREATE TABLE t29_acaorestricao (
  t29_cd_acaorestricao INT NOT NULL IDENTITY(1, 1),
  t07_cd_restricao INT NULL,
  t08_cd_acao INT NULL,
  dt_cadastro DATETIME NULL,
  PRIMARY KEY(t29_cd_acaorestricao)
);

-- FIM NOVAS TABELAS

select * from t08_acao

delete from t29_acaorestricao

select * from t29_acaorestricao where t07_cd_restricao=4