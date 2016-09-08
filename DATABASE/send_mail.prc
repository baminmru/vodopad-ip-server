create or replace procedure send_mail( p_sender IN varchar2,
                                       p_recipient IN varchar2,
                                       p_message IN varchar2)
as
  l_mailhost varchar(255) := '192.168.9.50';
  l_mail_conn utl_smtp.connection;
begin
     l_mail_conn := utl_smtp.open_connection(l_mailhost,25);
     utl_smtp.helo(l_mail_conn,l_mailhost);
     utl_smtp.mail(l_mail_conn,p_sender);
     utl_smtp.rcpt(l_mail_conn,p_recipient);
     utl_smtp.open_data(l_mail_conn);
     utl_smtp.write_data(l_mail_conn,p_message);
     utl_smtp.close_data(l_mail_conn);
     utl_smtp.quit(l_mail_conn);
end;
/

