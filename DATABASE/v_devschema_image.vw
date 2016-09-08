create or replace force view v_devschema_image as
select t.id_bd,D.name,d.schema_image,D.ds_id from bdevices t
join devschema D on (t.scheme_fn like '%\'|| to_char(D.ds_id) || '.bmp' or t.scheme_fn = to_char(D.ds_id) || '.bmp' );

