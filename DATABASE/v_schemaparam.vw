create or replace force view v_schemaparam as
select bdevices.id_bd,devschemaparam.NAME pname,edizm."EDIZM_ID",edizm."NAME",edizm."POSSIBLEPARAM",edizm."EDIZM_BASE",edizm."MULTIPICATOR",edizm."DIVIDER",devschemapipes.inputnumber, devschemapipes.pipenumber,pipetype.pipename  from  bdevices join devschema on bdevices.scheme_name = devschema.name
join devschemaparam on devschemaparam.ds_id = devschema.ds_id
left join devschemapipes on devschemaparam.dspipe_id =devschemapipes.dspipe_id
left join edizm on devschemaparam.edizm_id=edizm.Edizm_Id
left join pipetype  on devschemapipes.pipetype_id =pipetype.id_pipe;

