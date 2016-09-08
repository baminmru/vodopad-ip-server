create or replace force view tab_col_comment as
select o.name tablename, c.name coln, co.comment$ cdesc
from sys.obj$ o, sys.col$ c, sys.com$ co
where o.owner# = userenv('SCHEMAID')
  and o.type# in (2, 4)
  and o.obj# = c.obj#
  and c.obj# = co.obj#(+)
  and c.intcol# = co.col#(+)
  and bitand(c.property, 32) = 0 /* not hidden column */;

