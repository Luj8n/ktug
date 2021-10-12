#include<stdio.h>
#include<stdlib.h>
#include<string.h>

typedef struct coord {
    int x;
    int y;
} coord;

typedef struct queue {
    int popped;
    int length;
    int allocated;
    coord *data;
} queue;

coord coord_init(int x, int y) {
    coord c;
    c.x = x;
    c.y = y;
    int popped;
    return c;
}

queue queue_init() {
    queue q;
    q.popped = 0;
    q.length = 0;
    q.allocated = 100;
    q.data = malloc(100 * sizeof(coord));
    return q;
}

void queue_append(queue *q, coord val) {
    if (q->length == q->allocated) {
        q->allocated += 100;
        q->data = realloc(q->data, q->allocated * sizeof(coord));
    }
    q->data[q->length++] = val;
}

coord queue_pop(queue *q) {
    if (q->popped == q->length) {
        printf("causing some undefined behaviour\n");
        fflush(stdout);
    }
    return q->data[q->popped++];
}


void set_to(int arr[8][8], int val) {
    for (int i = 0; i < 8; i++) {
        for (int j = 0; j < 8; j++) {
            arr[i][j] = val;
        }
    }
}

int find_helper(char board[8][8], int map[8][8], queue *q, char queued[8][8], int startx, int starty, char target);

int find(char board[8][8], int map[8][8], int startx, int starty, char target);

void mark_path(char board[8][8], int map[8][8], int nums[8][8], int startx, int starty, int target);

int main() {
    int startx, starty;
    int endx, endy;
    int output;
    char board[8][8];
    int map[8][8];
    int nums[8][8];
    int path[8][8];
    for (int i = 0; i < 8; i++) {
        for (int j = 0; j < 8; j++) {
            scanf("%c ", &board[i][j]);
            if (board[i][j] == 'Z') {
                startx = i;
                starty = j;
            } else if (board[i][j] == 'K') {
                endx = i;
                endy = j;
            }
        }
    }
    memset(map, 0, sizeof(map));
    output = find(board, map, startx, starty, 'K');
    if (output == -1) {
        printf("no path found\n");
        return 0;
    }
    mark_path(board, map, nums, endx, endy, 0);
    set_to(map, output);
    if (find(board, map, endx, endy, 'Z') == -1) {
        printf("no path found\n");
        return 0;
    }
    mark_path(board, map, nums, startx, starty, output);
    printf("  ");
    for (int i = 0; i < 8; i++) {
        printf("%i ", i);
    }
    printf("\n");
    for (int i = 0; i < 8; i++) {
        printf("%i ", i);
        for (int j = 0; j < 8; j++) {
            if (board[i][j] == '0') {
                printf("- ");
            } else if (board[i][j] == 'c') {
                printf("%i ", nums[i][j]);
            } else {
                printf("%c ", board[i][j]);
            }
        }
        printf("\n");
    }
    return 0;
}

int find(char board[8][8], int map[8][8], int startx, int starty, char target) {
    char queued[8][8];
    coord c = coord_init(startx, starty);
    queue q = queue_init();
    memset(queued, 0, sizeof(queued));
    while (1) {
        if (find_helper(board, map, &q, queued, c.x, c.y, target)) {
            free(q.data);
            return map[c.x][c.y];
        }
        if (q.popped == q.length) {
            return -1;
        }
        c = queue_pop(&q);
    }
}


int find_helper(char board[8][8], int map[8][8], queue *q, char queued[8][8], int startx, int starty, char target) {
    if (board[startx][starty] == target) {
        return 1;
    }
    int xvals[8] = {
        startx - 2,
        startx - 2,
        startx + 2,
        startx + 2,
        startx - 1,
        startx - 1,
        startx + 1,
        startx + 1
    };
    int yvals[8] = {
        starty - 1,
        starty + 1,
        starty - 1,
        starty + 1,
        starty - 2,
        starty + 2,
        starty - 2,
        starty + 2
    };
    for (int i = 0; i < 8; i++) {
        if (xvals[i] < 0 || xvals[i] >= 8 || yvals[i] < 0 || yvals[i] >= 8) {
            continue;
        }
        if ((board[xvals[i]][yvals[i]] == '0' || board[xvals[i]][yvals[i]] == target) && !queued[xvals[i]][yvals[i]]) {
            map[xvals[i]][yvals[i]] = map[startx][starty] + 1;
            queued[xvals[i]][yvals[i]] = 1;
            queue_append(q, coord_init(xvals[i], yvals[i]));
        }
    }
    return 0;
}

void mark_path(char board[8][8], int map[8][8], int nums[8][8], int startx, int starty, int target) {
    if (map[startx][starty] - 1 == target) {
        return;
    }
    int xvals[8] = {
        startx - 2,
        startx - 2,
        startx + 2,
        startx + 2,
        startx - 1,
        startx - 1,
        startx + 1,
        startx + 1
    };
    int yvals[8] = {
        starty - 1,
        starty + 1,
        starty - 1,
        starty + 1,
        starty - 2,
        starty + 2,
        starty - 2,
        starty + 2
    };
    for (int i = 0; i < 8; i++) {
        if (xvals[i] < 0 || xvals[i] >= 8 || yvals[i] < 0 || yvals[i] >= 8) {
            continue;
        }
        if (map[xvals[i]][yvals[i]] == map[startx][starty] - 1) {
            board[xvals[i]][yvals[i]] = 'c';
            nums[xvals[i]][yvals[i]] = map[xvals[i]][yvals[i]];
            mark_path(board, map, nums, xvals[i], yvals[i], target);
            return;
        }
    }
}
