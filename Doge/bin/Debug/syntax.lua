element := '[' expr ']' '=' expr | name '=' expr | expr
element_sep := ',' | ';'
element_list := element { element_sep element }
unary_op := '#' | 'not' | '-'
binary_op := '^' | '*' | '/' | '%' | '+' | '-' | '.' '.' | '<' '=' | '<' | '>' '=' | '>' | '~' '=' | '=' '=' | 'and' | 'or'

stat_list := stat [ ';' ]
var_list := name { ',' name }
arg_list := '.' '.' '.' | name { ',' name } [ ',' '.' '.' '.' ]
expr_list := expr { ',' expr }

chunk := { stat_list } [ ret_stat ]
ret_stat := 'return' [ expr_list ] [ ';' ]
do_stat := 'do' [ chunk ] 'end'
while_stat := 'while' expr do_stat
for_stat := 'for' name '=' expr ',' expr [ ',' expr ] do_stat
foreach_stat := 'for' var_list 'in' [ expr_list ] do_stat
break_stat := 'break'
def_stat := 'local' var_list [ '=' expr_list ]
if_stat := 'if' expr 'then' [ chunk ] 'end'
if_else_stat := 'if' expr 'then' [ chunk ] 'else' [ chunk ] 'end'
if_elseif_stat := 'if' expr 'then' [ chunk ] { 'elseif' expr 'then' [ chunk ] } 'else' [ chunk ] 'end'
func_stat := [ 'local' ] 'function' name { '.' name } [ ':' name ] '(' [ arg_list ] ')' [ chunk ] 'end'
bind_stat := expr_list '=' expr_list
call_stat := call_expr

func_expr := 'function' '(' [ arg_list ] ')' [ chunk ] 'end'
paren_expr := '(' expr ')'
table_expr := '{' [ element_list ] '}'
prop_expr := expr '.' name
index_expr := expr '[' expr ']'
call_expr := expr '(' [ expr_list ] ')'
unary_expr := unary_op expr
binary_expr := expr binary_op expr
