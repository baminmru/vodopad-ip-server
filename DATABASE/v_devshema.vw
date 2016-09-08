create or replace force view v_devshema as
select d.cdevdesc,d.cdevname,c.fld20, c.fld52,u.cshort,u.cfull,
--w.cname,t.cname t_cname ,
decode(fld52,'ÑÈ-1','SI1','ÑÈ-2','SI2','ÑÈ-5','SI5','ÑÈ-6','SI6','ÑÏ-0','SI1',
'ÑÏ-1','SI1','ÑÏ-2','SI2','ÑÏ-5','SI5','ÑÏ-6','SI6','CÏ-7','SI7','CÏ 7','SI7')  || decode(fld108,'-','','TV2','TV2')   proc_prefix,
decode(cdevname,'MT200116','MT200','SPT942','SPT942','SPT943','SPT943','SPT960','SPT960')  proc_postfix
,c.fld12 vich_num
,c.fld51 EQType
,b.id_bd
,b.reportproc
from bdevices b
join devices d on b.id_dev = d.id_dev
left join contract c on b.id_bd = c.id_bd
join bbuildings u on b.id_bu = u.id_bu
--join whogive w on u.id_who = w.id_who
--join whogivetop t on t.id_whotop = w.id_whotop
;

