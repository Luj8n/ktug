import Data.List as L
import qualified Data.Set as S

type LetterCoordinates = [(Int, Int)]


main :: IO()
main = do
  lenx <- readLn
  leny <- readLn
  array <- readLines lenx
  strings <- readLines lenx
  let
    foundStrings = findStrings (reverse strings) (reverse array)
    possibleSetup = changeFormat lenx leny $ findPossibleSetup foundStrings
  putStrLn $ show $ map (foldr (+) 0) $ L.transpose possibleSetup


readLines :: Int -> IO [String]
readLines = helper []
  where
    helper xs 0 = return xs
    helper xs n = do
      x <- getLine
      res <- helper (x:xs) (n - 1)
      return res

findStrings :: [String] -> [String] -> [[LetterCoordinates]]
findStrings strs arr = map (\str -> findString str arr) strs


findString :: String -> [String] -> [LetterCoordinates]
findString str arr = concat [maybeFindString str arr (i, j) [] | (i, s) <- zip [0..] arr, (j, _) <- zip [0..] s]


maybeFindString :: String -> [String] -> (Int, Int) -> LetterCoordinates -> [LetterCoordinates]
maybeFindString (c:[]) arr (x, y) output
  | Just arr `get` x `get` y /= Just c = []
  | otherwise = [(x, y) : output]
maybeFindString (c:cs) arr (x, y) output
  | Just arr `get` x `get` y /= Just c = []
  | otherwise = concat [maybeFindString cs arr xy' ((x, y) : output) | xy' <- posxys]
  where
    posxys = [(x - 1, y), (x + 1, y), (x, y - 1), (x, y + 1)]

get :: Maybe [a] -> Int -> Maybe a
Nothing `get` _ = Nothing
Just xs `get` x = if x >= 0 && x < length xs then Just (xs !! x) else Nothing


findPossibleSetup :: [[LetterCoordinates]] -> [LetterCoordinates]
findPossibleSetup = removeDups . getAllCombinations


getAllCombinations :: [[LetterCoordinates]] -> [[LetterCoordinates]]
getAllCombinations (x:[]) = [[i] | i <- x]
getAllCombinations (x:xs) = concat [map (i:) (getAllCombinations xs) | i <- x]


removeDups :: [[LetterCoordinates]] -> [LetterCoordinates]
removeDups = head . filter ((filterer S.empty) . concat)
  where
    filterer _ [] = True
    filterer set (x:xs)
      | x `S.member` set = False
      | otherwise = filterer (x `S.insert` set) xs


changeFormat :: Int -> Int -> [LetterCoordinates] -> [[Int]]
changeFormat sizex sizey = iteratingReplacer ((take sizex . repeat . take sizey . repeat) 0) [1..]
  where
    iteratingReplacer arr _ [] = arr
    iteratingReplacer arr (v:vs) (coord:coords) = iteratingReplacer (replacer arr v coord) vs coords
    replacer arr _ [] = arr
    replacer arr v ((x, y):xys) = replacer (replaceIndex2D x y v arr) v xys


replaceIndex2D :: Int -> Int -> a -> [[a]] -> [[a]]
replaceIndex2D = helper 0
  where
    helper i' i j v (x:xs)
      | i == i' = (replaceIndex j v x) : xs
      | otherwise = x : helper (i' + 1) i j v xs


replaceIndex :: Int -> a -> [a] -> [a]
replaceIndex = helper 0
  where
    helper i' i v (x:xs)
      | i == i' = v : xs
      | otherwise = x : helper (i' + 1) i v xs
