2 constant max-clients
create pollfds max-clients 1+ pollfd% * allot
pollfds max-clients 1+ pollfd% * -1 fill
pollfds 0pollfd

: pollfds-do
   pollfds pollfd% + max-clients pollfd% * bounds
   POSTPONE 2literal POSTPONE do ; immediate
  ( -- ) \ current client pollfd in I
: pollfds-loop
   pollfd% POSTPONE literal POSTPONE +loop ; immediate

: clients-do
   POSTPONE pollfds-do
      POSTPONE i POSTPONE pollfd.fd@ POSTPONE 0>= POSTPONE if ;
immediate
  ( -- ) \ current client pollfd in I
: clients-loop
   POSTPONE then POSTPONE pollfds-loop ; immediate

: pollfd.kill ( pollfd -- )
   dup pollfd.fd@ close-socket drop
   -1 swap pollfd.fd! ;

: >pollfds ( client-sock -- )
   pollfds-do
      i pollfd.fd@ 0< if
         ( client-sock ) i pollfd.fd!
         i 0pollfd
         unloop exit
      then
   pollfds-loop
   ( client-sock ) dup s\" go away, we're full\r\n" 0 send ?oops drop
   close-socket ?oops drop ;

: #pollfds ( -- clients )
   0 clients-do 1+ clients-loop ;

: broadcast ( c-addr u -- )
   clients-do
      2dup i pollfd.fd@ -rot 0 send ?oops drop
   clients-loop 2drop ;

: .clients ( -- )
   cr ." Broadcasting with " #pollfds . ." clients" ;

: chat-server ( -- )
   4004 server-socket ?oops pollfds pollfd.fd!
   begin
      pollfds max-clients 1+ -1 poll ?oops drop
      pollfds pollfd-ready? if
         pollfds pollfd.fd@ accept-client ?oops >pollfds
         .clients
      then
      clients-do
         i pollfd-ready? if
            i pollfd.fd@ here 4096 0 recv ?oops ?dup if
               here swap broadcast
            else
               i pollfd.kill
               .clients
            then
         then
      clients-loop
   again ;
