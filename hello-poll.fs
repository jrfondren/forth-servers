create pollfds pollfd% allot
pollfds 0pollfd

: hello-server ( -- )
   4003 server-socket ?oops pollfds pollfd.fd!
   begin
      pollfds 1 -1 poll ?oops drop
      pollfds pollfd-ready? if
         pollfds pollfd.fd@ accept-client ?oops
         dup s\" Hello, world!\r\n" 0 send ?oops drop
         close-socket ?oops drop
      then
   again ;
