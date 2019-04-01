: hello-server ( -- )
   4001 server-socket ?oops
   begin
      dup accept-client ?oops
      dup s\" Hello, world!\r\n" 0 send ?oops drop
      close-socket ?oops drop
   again ;
