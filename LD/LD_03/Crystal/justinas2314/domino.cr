def find(data, trail, used)
    if trail.size == 7
        puts trail.join ' '
        return
    end
    (0..6).each do |i|
        if used[i]
            next
        end
        used[i] = true
        if trail[-1][1] == data[i][0]
            find(data, trail + [data[i]], used)
        end
        if trail[-1][1] == data[i][1]
            find(data, trail + [data[i].reverse], used)
        end
        used[i] = false
    end
end

data = gets.to_s.chomp.split(' ')
(0..6).each do |i|
    used = [false] * 7
    used[i] = true
    find(data, data[i..i], used)
    find(data, [data[i].reverse], used)
end