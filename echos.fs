: echo-server ( -- )
   4002 server-socket ?oops
   begin
      dup accept-client ?oops fork 0= if
         begin
            dup here 1024 0 recv ?oops dup
         while
            over here rot 0 send ?oops drop
         repeat drop
         close-socket ?oops drop
         bye
      then drop reap
   again ;
