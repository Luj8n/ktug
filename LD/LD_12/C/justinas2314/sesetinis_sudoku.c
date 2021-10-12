#include<stdio.h>

int fill_square(int board[6][6], int x, int y);

int main() {
    int temp;
    int board[6][6];
    int blank_count = 0;
    for (int i = 0; i < 6; i++) {
        for (int j = 0; j < 6; j++) {
           scanf("%i", &board[i][j]);
           if (board[i][j] == 0) {
               blank_count++;
           }
        }
    }
    while (blank_count > 0) {
        for (int i = 0; i < 6; i++) {
            for (int j = 0; j < 6; j++) {
                if (board[i][j] != 0) {
                    continue;
                }
                temp = fill_square(board, i, j);
                if (temp) {
                    board[i][j] = temp;
                    blank_count--;
                }
            }
        }
    }
    for (int i = 0; i < 6; i++) {
        for (int j = 0; j < 6; j++) {
           printf("%i ", board[i][j]);
        }
        printf("\n");
    }
    return 0;
}

int fill_square(int board[6][6], int x, int y) {
    int sx;
    int sy;
    char flag = 1;
    int total = 0;
    for (int i = 0; i < 6; i++) {
        if (i == y) {
            continue;
        } else if (board[x][i] == 0) {
            flag = 0;
            break;
        }
        total += board[x][i];
    }
    if (flag) {
        return 21 - total;
    } else {
        flag = 1;
        total = 0;
    }
    for (int i = 0; i < 6; i++) {
        if (i == x) {
            continue;
        } else if (board[i][y] == 0) {
            flag = 0;
            break;
        }
        total += board[i][y];
    }
    if (flag) {
        return 21 - total;
    } else {
        flag = 1;
        total = 0;
    }
    sx = x / 2;
    sy = y / 3;
    for (int i = sx; i < sx + 2; i++) {
        for (int j = sy; j < sy + 3; j++) {
            if (i == x && j == y) {
                continue;
            } else if (board[i][j] == 0) {
                return 0;
            }
            total += board[i][j];
        }
    }
    return 21 - total;
}
