create or replace package owa_sylk as
--
  type owaSylkArray is table of varchar2(2000);
--
  procedure show(
      p_file          in utl_file.file_type,
      p_query         in varchar2,
      p_parm_names    in owaSylkArray default owaSylkArray(),
      p_parm_values   in owaSylkArray default owaSylkArray(),
      p_sum_column    in owaSylkArray default owaSylkArray(),
      p_max_rows      in number     default 10000,
      p_show_null_as  in varchar2   default null,
      p_show_grid     in varchar2   default 'YES',
      p_show_col_headers in varchar2 default 'YES',
      p_font_name     in varchar2   default 'Courier New',
      p_widths        in owaSylkArray default owaSylkArray(),
      p_titles        in owaSylkArray default owaSylkArray(),
      p_strip_html    in varchar2   default 'YES',
      p_user_heading  in varchar2   default null );
--
  procedure show(
      p_file          in utl_file.file_type,
      p_cursor        in integer,
      p_sum_column    in owaSylkArray  default owaSylkArray(),
      p_max_rows      in number     default 10000,
      p_show_null_as  in varchar2   default null,
      p_show_grid     in varchar2   default 'YES',
      p_show_col_headers in varchar2 default 'YES',
      p_font_name     in varchar2   default 'Courier New',
      p_widths        in owaSylkArray default owaSylkArray(),
      p_titles        in owaSylkArray default owaSylkArray(),
      p_strip_html    in varchar2   default 'YES',
      p_user_heading  in varchar2   default null );
--
end owa_sylk;
/

