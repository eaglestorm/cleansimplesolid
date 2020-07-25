create table css_user (
  id bigserial primary key, --internal record id
  subject varchar(255) unique, -- open id subject identifier.
  fullName varchar(255) not null,
  email varchar(100) not null,
  createdDate timestamp not null,
  modifiedDate timestamp not null          
);

create table task (
  id bigserial primary key,
  createdBy bigint references css_user,  --the owner.
  name varchar(100) not null,
  createdDate timestamp not null,
  modifiedDate timestamp not null,
  scheduledDate date not null,
  dueDate date not null,
  description text not null
)