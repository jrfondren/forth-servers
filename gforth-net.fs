require lib.fs

library libserver ./libserverlib.so
libserver server-socket int (int) server_socket
libserver accept-client int (int) accept_client
libserver reap (void) reap
libserver pollfd.fd! int ptr (void) set_pollfd_fd
libserver pollfd.fd@ ptr (int) get_pollfd_fd
libserver 0pollfd ptr (void) init_pollfd
libserver pollfd-bad? ptr (int) is_pollfd_bad
libserver pollfd-ready? ptr (int) is_pollfd_ready

library libc libc.so.6
libc send int ptr int int (int) send
libc recv int ptr int int (int) recv
libc close-socket int (int) close
libc fork (int) fork
libc poll ptr int int (int) poll

$1 constant POLLIN
$8 constant POLLERR
$10 constant POLLHUP
$20 constant POLLNVAL

8 constant pollfd%

: ?oops ( n -- n )
   dup 0< abort" something failed :(" ;
