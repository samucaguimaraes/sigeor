alter table dbo.t05_parceiro add fl_entidade bit null

select * from t09_marco where fl_ativa=1 and t08_cd_acao in (select t08_cd_acao from t08_acao where fl_ativa=1 and t03_cd_projeto in (select t03_cd_projeto from t03_projeto where fl_ativa=1))
select * from t09_marco where fl_ativa=1 and dt_realizada is not null and t08_cd_acao in (select t08_cd_acao from t08_acao where fl_ativa=1 and t03_cd_projeto in (select t03_cd_projeto from t03_projeto where fl_ativa=1))


select * from t09_marco where fl_ativa=1 and 
(dt_realizada is null) 
and t08_cd_acao in (select t08_cd_acao from t08_acao where fl_ativa=1 and t03_cd_projeto in (select t03_cd_projeto from t03_projeto where fl_ativa=1 and t01_cd_entidade=)) order by dt_prevista DESCand (dt_realizada is null) and t08_cd_acao in (select t08_cd_acao from t08_acao where fl_ativa=1 and t03_cd_projeto in (select t03_cd_projeto from t03_projeto where fl_ativa=1 and t01_cd_entidade=)) order by dt_prevista ASC 


delete from t01_entidade where t01_cd_entidade=10

select * from t01_entidade
select * from t05_parceiro
select * from dbo.t25_perfil

select * from t03_projeto where t01_cd_entidade=5
update t03_projeto set dt_alterado = '2008-06-01' where t03_cd_projeto=11

