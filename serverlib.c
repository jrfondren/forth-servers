#define _GNU_SOURCE
#include <stdio.h>
#include <sys/types.h>
#include <sys/wait.h>
#include <sys/socket.h>
#include <netdb.h>
#include <string.h>
#include <poll.h>

long server_socket (int port_int) {
	int sock;
	int yes = 1;
	struct addrinfo hints, *res;
	char port[10];

	if (snprintf(port, sizeof port, "%d", port_int) >= sizeof port)
		return -1;

	memset(&hints, 0, sizeof hints);
	hints.ai_family = AF_UNSPEC;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_flags = AI_PASSIVE;
	getaddrinfo(NULL, port, &hints, &res);

	if (-1 == (sock = socket(res->ai_family, res->ai_socktype, res->ai_protocol)))
		return -1;
	if (-1 == setsockopt(sock, SOL_SOCKET, SO_REUSEADDR, &yes, sizeof yes))
		return -1;
	if (-1 == bind(sock, res->ai_addr, res->ai_addrlen))
		return -1;
	if (-1 == listen(sock, 4))
		return -1;

	return sock;
}

long accept_client (int sock) {
	struct sockaddr ignored;
	socklen_t ignored_size = sizeof ignored;
	return accept4(sock, &ignored, &ignored_size, SOCK_CLOEXEC);
}

void reap (void) {
	int wstatus;
	waitpid(-1, &wstatus, WNOHANG);
}

void set_pollfd_fd (int fd, struct pollfd *pfd) { pfd->fd = fd; }
long get_pollfd_fd (struct pollfd *pfd) { return pfd->fd; }
void init_pollfd (struct pollfd *pfd) { pfd->events = POLLIN; }
long is_pollfd_bad (struct pollfd *pfd) { return (pfd->revents & (POLLERR | POLLHUP | POLLNVAL)) ? -1 : 0; }
long is_pollfd_ready (struct pollfd *pfd) { return (pfd->revents & POLLIN) ? -1 : 0; }
long wtf_int (void) { return -1; }
