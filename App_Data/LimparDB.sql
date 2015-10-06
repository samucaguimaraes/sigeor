use dbSebraeParceiro;

delete from t01_entidade;
delete from t02_usuario where t02_cd_usuario<>'adm';
delete from t03_projeto;
delete from t05_parceiro;
delete from t06_parceiroprojeto;
delete from t07_restricao;
delete from t08_acao;
delete from t09_marco;
delete from t10_produto;
delete from t11_financeiro;
delete from t12_premissa;
delete from t13_foco;
delete from t14_resultado;
delete from t15_vlresultado;
delete from t16_situacao;
delete from t17_colaborador;
delete from t18_documento;
delete from t20_faseprojeto;
delete from t21_acesso;
delete from t22_log;
delete from t23_noticia;
delete from t24_agenda;
delete from t26_usuarioperfil where t02_cd_usuario<>'adm';
delete from t27_vlproduto;
delete from t28_vlfinanceiro;
delete from t29_acaorestricao;


